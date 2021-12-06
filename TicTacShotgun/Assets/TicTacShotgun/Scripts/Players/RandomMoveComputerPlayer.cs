using TicTacShotgun.Simulation;

namespace TicTacShotgun.Players
{
    public class RandomMoveComputerPlayer : Player
    {
        public RandomMoveComputerPlayer(int index, Board board) : base(index, board)
        {
        }
        
        public override void OnTurnStart()
        {
            var randomIndex = Board.GetRandomUnoccupiedIndex();
            
            RequestMove(randomIndex);
        }

        public override void OnTurnEnd()
        {
            
        }
    }
}