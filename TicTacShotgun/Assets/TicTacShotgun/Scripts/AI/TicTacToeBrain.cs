using TicTacShotgun.Players;

namespace TicTacShotgun.Simulation
{
    public abstract class TicTacToeBrain
    {
        protected Board Board;
        
        public TicTacToeBrain(Board board)
        {
            Board = board;
        }

        public abstract Board.Index GetNextMove(Player player);
    }
}