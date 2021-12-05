using System;
using TicTacShotgun.GameFlow;

namespace TicTacShotgun.Simulation
{
    public class TicTacToeGame
    {
        readonly Board board;

        public event Action<GameBoardState> OnGameFinished = s => { };
        public Board Board => board;
        
        public TicTacToeGame(Board board)
        {
            this.board = board;
            
            board.OnMovePerformed += OnMovePerformed;
        }

        void OnMovePerformed(Move move)
        {
            var boardState = board.EvaluateCurrentBoardState();

            if (boardState == GameBoardState.GameInProgress)
            {
                return;
            }
            
            OnGameFinished.Invoke(boardState);
        }
    }
}