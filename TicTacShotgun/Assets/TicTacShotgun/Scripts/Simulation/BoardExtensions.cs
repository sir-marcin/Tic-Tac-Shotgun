using TicTacShotgun.GameFlow;

namespace TicTacShotgun.Simulation
{
    public static class BoardExtensions
    {
        public static GameBoardState EvaluateCurrentBoardState(this Board board)
        {
            return GameBoardState.GameInProgress;
        }
    }
}