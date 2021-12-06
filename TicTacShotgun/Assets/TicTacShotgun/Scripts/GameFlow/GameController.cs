using System;
using TicTacShotgun.PlayerInput;
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

        public VisualConfig VisualConfig => visualConfig;
        public TicTacToeGame CurrentGameInstance => currentGameInstance;
        public PlayerController PlayerController => playerController;

        void Start()
        {
            var board = new Board();
            currentGameInstance = new TicTacToeGame(board);
            currentGameInstance.OnGameFinished += OnGameInstanceFinished;

            playerMoveHandler = new PlayerMoveHandler(board);
            
            var playerIndexFactory = new PlayerIndexFactory();
            playerController = new PlayerController(visualConfig, 
                new HumanLocalPlayer(playerIndexFactory.GetNextIndex(), board), 
                new HumanLocalPlayer(playerIndexFactory.GetNextIndex(), board));
            
            OnGameStarted.Invoke(this);
        }

        void OnDestroy()
        {
            playerController.Dispose();
            playerMoveHandler.Dispose();
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
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}