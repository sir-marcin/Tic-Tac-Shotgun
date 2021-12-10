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
        HintsController hintsController;
        GameMode currentGameMode;

        public VisualConfig VisualConfig => visualConfig;
        public TicTacToeGame CurrentGameInstance => currentGameInstance;
        public PlayerController PlayerController => playerController;
        public BoardHistoryController BoardHistoryController => boardHistoryController;
        public HintsController HintsController => hintsController;
        public GameMode CurrentGameMode => currentGameMode;

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
            currentGameMode = GameModeData.SelectedGameMode;
            
            var board = new Board();
            var hintsBrain = new RandomMoveBrain(board);
            hintsController = new HintsController(hintsBrain);
            boardHistoryController = new BoardHistoryController(board);
            currentGameInstance = new TicTacToeGame(board);
            playerMoveHandler = new PlayerMoveHandler(board);
            playerController = new PlayerController(visualConfig, board, currentGameMode);
            turnController = new TurnController(playerController, currentGameInstance);
            
            currentGameInstance.OnGameFinished += OnGameInstanceFinished;
            
            OnGameStarted.Invoke(this);
        }

        void DisposeGame()
        {
            if (currentGameInstance == null)
            {
                return;
            }
            
            currentGameInstance.OnGameFinished -= OnGameInstanceFinished;
            
            currentGameInstance.Dispose();
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