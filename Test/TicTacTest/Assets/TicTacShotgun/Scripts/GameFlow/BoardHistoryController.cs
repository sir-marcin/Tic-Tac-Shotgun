using System;
using System.Collections.Generic;
using TicTacShotgun.Simulation;

namespace TicTacShotgun.GameFlow
{
    public class BoardHistoryController : IDisposable
    {
        readonly Board board;
        readonly Stack<Move> movesHistory = new Stack<Move>();
        
        public BoardHistoryController(Board board)
        {
            this.board = board;
            
            board.OnMovePerformed += OnMovePerformed;
        }

        void OnMovePerformed(Move move)
        {
            movesHistory.Push(move);
        }

        void TryUndoLastMove()
        {
            if (movesHistory.Count > 0)
            {
                board.UndoMove(movesHistory.Pop());
            }
        }
        
        public void UndoLastMove()
        {
            // undo last 2 moves because we want to undo AI move as well
            TryUndoLastMove();
            TryUndoLastMove();
        }
        
        public void Dispose()
        {
            movesHistory.Clear();
            
            if (board != null)
            {
                board.OnMovePerformed -= OnMovePerformed;
            }
        }
    }
}