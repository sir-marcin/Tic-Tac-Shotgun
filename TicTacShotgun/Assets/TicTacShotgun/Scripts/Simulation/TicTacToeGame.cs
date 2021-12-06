using System;
using TicTacShotgun.GameFlow;

namespace TicTacShotgun.Simulation
{
    public class TicTacToeGame
    {
        readonly Board board;

        public event Action OnNextMoveAvailable = () => { };
        public event Action<GameEndDetails> OnGameFinished = d => { };
        public Board Board => board;
        
        public TicTacToeGame(Board board)
        {
            this.board = board;
            
            board.OnMovePerformed += OnMovePerformed;
        }

        void OnMovePerformed(Move move)
        {
            var boardState = board.EvaluateCurrentBoardState();

            switch (boardState)
            {
                case GameBoardState.GameInProgress:
                    OnNextMoveAvailable.Invoke();
                    break;
                case GameBoardState.Draw:
                    OnGameFinished.Invoke(new GameEndDetails(boardState));
                    break;
                case GameBoardState.Win:
                    OnGameFinished.Invoke(new GameEndDetails(boardState, move.Player));
                    break;
            }
        }
    }
}