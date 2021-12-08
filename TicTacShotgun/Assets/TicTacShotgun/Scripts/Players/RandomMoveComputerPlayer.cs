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
            var boardIndex = Board.GetRandomUnoccupiedIndex();
            RequestMove(boardIndex);
        }

        public override void OnTurnEnd()
        {
            
        }
    }
}