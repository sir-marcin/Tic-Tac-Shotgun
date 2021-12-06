using System;
using TicTacShotgun.BoardView;
using TicTacShotgun.Simulation;

namespace TicTacShotgun.Players
{
    public abstract class Player : IDisposable
    {
        protected Board Board;
        
        public event Action<Move> OnMoveRequested = m => { };
        public int Index { get; }

        public Player(int index, Board board)
        {
            Board = board;
            Index = index;
        }

        protected void RequestMove(Board.Index index)
        {
            RequestMove(index.X, index.Y);
        }
        
        protected void RequestMove(BoardField boardField)
        {
            RequestMove(boardField.Index.X, boardField.Index.Y);
        }
        
        protected void RequestMove(int x, int y)
        {
            var move = new Move(x, y, this);
            OnMoveRequested.Invoke(move);
        }

        public virtual void Dispose()
        {
        }

        public abstract void OnTurnStart();
        public abstract void OnTurnEnd();
    }
}