using System;
using TicTacShotgun.GameFlow;

namespace TicTacShotgun.Simulation
{
    public class TicTacToeGame
    {
        readonly Board board;

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

            if (boardState == GameBoardState.GameInProgress)
            {
                return;
            }

            var gameEndDetails = new GameEndDetails(boardState, move.Player);
            
            OnGameFinished.Invoke(gameEndDetails);
        }
    }
}