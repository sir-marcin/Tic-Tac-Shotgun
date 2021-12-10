using TicTacShotgun.Players;
using TicTacShotgun.Simulation;

namespace TicTacShotgun
{
    public class HintsController
    {
        readonly TicTacToeBrain brain;
        
        public HintsController(TicTacToeBrain brain)
        {
            this.brain = brain;
        }

        public Board.Index GetNextHint(Player player)
        {
            return brain.GetNextMove(player);
        }
    }
}