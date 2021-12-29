using TicTacShotgun.Players;
using TicTacShotgun.Simulation;

namespace TicTacShotgun.GameFlow
{
    public class GameEndDetails
    {
        public readonly GameBoardState BoardState;
        public readonly Player Champion;

        public GameEndDetails(GameBoardState boardState, Player champion = null)
        {
            BoardState = boardState;
            Champion = champion;
        }
    }

    public enum GameBoardState
    {
        GameInProgress = 0,
        Draw = 1,
        Win = 2
    }
}