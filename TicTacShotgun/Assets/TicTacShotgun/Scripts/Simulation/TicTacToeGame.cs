using System;
using TicTacShotgun.GameFlow;

namespace TicTacShotgun.Simulation
{
    public class TicTacToeGame : IDisposable
    {
        readonly Board board;
        GameBoardState currentGameState;
        
        public event Action OnNextMoveAvailable = () => { };
        public event Action<GameEndDetails> OnGameFinished = d => { };
        public Board Board => board;
        public GameBoardState CurrentGameState => currentGameState;

        public TicTacToeGame(Board board)
        {
            this.board = board;
            currentGameState = GameBoardState.GameInProgress;
            
            board.OnMovePerformed += OnMovePerformed;
        }

        public void Dispose()
        {
            if (board != null)
            {
                board.OnMovePerformed -= OnMovePerformed;
            }
        }

        void OnMovePerformed(Move move)
        {
            currentGameState = board.EvaluateCurrentBoardState();

            switch (currentGameState)
            {
                case GameBoardState.GameInProgress:
                    OnNextMoveAvailable.Invoke();
                    break;
                case GameBoardState.Draw:
                    OnGameFinished.Invoke(new GameEndDetails(currentGameState));
                    break;
                case GameBoardState.Win:
                    OnGameFinished.Invoke(new GameEndDetails(currentGameState, move.Player));
                    break;
            }
        }
    }
}