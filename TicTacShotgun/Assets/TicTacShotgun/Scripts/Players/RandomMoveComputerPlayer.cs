using System.Threading;
using System.Threading.Tasks;
using TicTacShotgun.Simulation;

namespace TicTacShotgun.Players
{
    public class RandomMoveComputerPlayer : Player
    {
        const int THINKING_TIME_MILLISECONDS = 500;
        readonly TicTacToeBrain brain;
        Task<Board.Index> brainTask;
        
        public RandomMoveComputerPlayer(int index, Board board) : base(index, board)
        {
            brain = new RandomMoveBrain(board);
        }
        
        public override void OnTurnStart()
        {
            brainTask = Task.Factory.StartNew(GetNextMove);
        }

        public override void OnTurnEnd()
        {
        }

        public override void Update()
        {
            if (brainTask.IsCompleted)
            {
                var calculatedMove = brainTask.Result;
                RequestMove(calculatedMove);
            }
        }

        Board.Index GetNextMove()
        {
            Thread.Sleep(THINKING_TIME_MILLISECONDS);
            return brain.GetNextMove(this);
        }
    }
}