using System;
using UnityEngine;

namespace TicTacShotgun.Simulation
{
    public abstract class Player : IDisposable
    {
        public event Action<Move> OnMovePerformed = m => { };
        public int Index { get; }

        public Player(int index)
        {
            Index = index;
        }
        
        protected void Move(int x, int y)
        {
            var move = new Move(x, y, Index);
            OnMovePerformed.Invoke(move);
        }

        public virtual void Dispose()
        {
        }
    }
}