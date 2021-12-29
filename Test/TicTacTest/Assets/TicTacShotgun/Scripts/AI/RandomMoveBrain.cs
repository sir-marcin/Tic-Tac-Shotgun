using TicTacShotgun.Players;

namespace TicTacShotgun.Simulation
{
    public class RandomMoveBrain : TicTacToeBrain
    {
        public RandomMoveBrain(Board board) : base(board)
        {
        }

        public override Board.Index GetNextMove(Player player)
        {
            return Board.GetRandomUnoccupiedIndex();
        }
    }
}