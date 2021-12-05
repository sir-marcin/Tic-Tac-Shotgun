using System;
using TicTacShotgun.Simulation;
using UnityEngine;

namespace TicTacShotgun.GameFlow
{
    public class GameController : MonoBehaviour
    {
        public static event Action<GameController> OnGameStarted = gc => { }; // by default I assign empty methods to avoid nullchecks when invoking
        public static event Action<GameEndDetails> OnGameEnded = d => { };

        [SerializeField] VisualConfig visualConfig;

        PlayerController playerController;
        TicTacToeGame currentGameInstance;

        public VisualConfig VisualConfig => visualConfig;
        public TicTacToeGame CurrentGameInstance => currentGameInstance;
        public PlayerController PlayerController => playerController;

        void Start()
        {
            playerController = new PlayerController(visualConfig, new HumanPlayer(), new HumanPlayer());

            // code ready for future loading of saved board state
            var board = new Board();
            currentGameInstance = new TicTacToeGame(board);
            currentGameInstance.OnGameFinished += OnGameInstanceFinished;
            
            OnGameStarted.Invoke(this);
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