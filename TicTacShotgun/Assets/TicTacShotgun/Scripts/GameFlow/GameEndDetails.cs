namespace TicTacShotgun.GameFlow
{
    public class GameEndDetails
    {
        public GameController GameController { get; set; }
        public readonly GameBoardState BoardState;

        public GameEndDetails(GameBoardState boardState)
        {
            BoardState = boardState;
        }
    }

    public enum GameBoardState
    {
        GameInProgress = 0,
        Draw = 1,
        P1Win = 2,
        P2Win = 3
    }
}