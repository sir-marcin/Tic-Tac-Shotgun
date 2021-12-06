using System;
using TicTacShotgun.BoardView;
using TicTacShotgun.Simulation;

namespace TicTacShotgun.Players
{
    public abstract class Player : IDisposable
    {
        protected Board Board;
        
        public event Action<Move> OnMovePerformed = m => { };
        public int Index { get; }

        public Player(int index, Board board)
        {
            Board = board;
            Index = index;
        }

        protected void Move(BoardField boardField)
        {
            Move(boardField.Index.X, boardField.Index.Y);
        }
        
        protected void Move(int x, int y)
        {
            var move = new Move(x, y, this);
            OnMovePerformed.Invoke(move);
        }

        public virtual void Dispose()
        {
        }

        public abstract void OnTurnStart();
        public abstract void OnTurnEnd();
    }
}