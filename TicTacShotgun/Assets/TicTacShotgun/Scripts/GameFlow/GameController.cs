using System;
using TicTacShotgun.PlayerInput;
using TicTacShotgun.Simulation;
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
        HumanPlayerInputMediator humanPlayerInputMediator;

        public VisualConfig VisualConfig => visualConfig;
        public TicTacToeGame CurrentGameInstance => currentGameInstance;
        public PlayerController PlayerController => playerController;

        void Start()
        {
            var board = new Board();
            currentGameInstance = new TicTacToeGame(board);
            currentGameInstance.OnGameFinished += OnGameInstanceFinished;

            humanPlayerInputMediator = new HumanPlayerInputMediator(board);
            
            var playerIndexFactory = new PlayerIndexFactory();
            playerController = new PlayerController(visualConfig, new HumanLocalPlayer(playerIndexFactory.GetNextIndex()), new HumanLocalPlayer(playerIndexFactory.GetNextIndex()));
            
            OnGameStarted.Invoke(this);
        }

        void OnDestroy()
        {
            playerController.Dispose();
            humanPlayerInputMediator.Dispose();
        }

        void OnGameInstanceFinished(GameBoardState state)
        {
            var gameEndDetails = new GameEndDetails(state)
            {
                GameController = this
            };
            
            OnGameEnded.Invoke(gameEndDetails);
        }
    }
}