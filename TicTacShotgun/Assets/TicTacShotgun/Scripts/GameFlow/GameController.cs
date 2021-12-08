using System;
using TicTacShotgun.PlayerInput;
using TicTacShotgun.Players;
using TicTacShotgun.Simulation;
using TicTacShotgun.Utils;
using UnityEngine;

namespace TicTacShotgun.GameFlow
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] VisualConfig visualConfig;
        
        public static event Action<GameController> OnGameStarted = gc => { }; // by default I assign empty methods to avoid nullchecks when invoking
        public static event Action<GameEndDetails> OnGameEnded = d => { };
        
        PlayerController playerController;
        TicTacToeGame currentGameInstance;
        PlayerMoveHandler playerMoveHandler;
        TurnController turnController;
        BoardHistoryController boardHistoryController;

        public VisualConfig VisualConfig => visualConfig;
        public TicTacToeGame CurrentGameInstance => currentGameInstance;
        public PlayerController PlayerController => playerController;
        public BoardHistoryController BoardHistoryController => boardHistoryController;

        void Start()
        {
            InitializeGame();
        }

        void OnDestroy()
        {
            DisposeGame();
        }

        void InitializeGame()
        {
            var board = new Board();
            boardHistoryController = new BoardHistoryController(board);
            currentGameInstance = new TicTacToeGame(board);
            currentGameInstance.OnGameFinished += OnGameInstanceFinished;

            playerMoveHandler = new PlayerMoveHandler(board);
            
            var playerIndexFactory = new PlayerIndexFactory();
            playerController = new PlayerController(visualConfig, 
                new HumanLocalPlayer(playerIndexFactory.GetNextIndex(), board), 
                new RandomMoveComputerPlayer(playerIndexFactory.GetNextIndex(), board));

            turnController = new TurnController(playerController, currentGameInstance);
            
            OnGameStarted.Invoke(this);
        }

        void DisposeGame()
        {
            currentGameInstance.OnGameFinished -= OnGameInstanceFinished;
            
            boardHistoryController.Dispose();
            playerController.Dispose();
            playerMoveHandler.Dispose();
            turnController.Dispose();
        }
        
        void OnGameInstanceFinished(GameEndDetails gameEndDetails)
        {
            OnGameEnded.Invoke(gameEndDetails);

            switch (gameEndDetails.BoardState)
            {
                case GameBoardState.Draw:
                    TicTacLogger.Log($"Game ended with a draw!");
                    break;
                case GameBoardState.Win:
                    TicTacLogger.Log($"Player {gameEndDetails.Champion?.Index} won!");
                    break;
            }
        }

        public void Restart()
        {
            DisposeGame();
            InitializeGame();
        }
    }
}