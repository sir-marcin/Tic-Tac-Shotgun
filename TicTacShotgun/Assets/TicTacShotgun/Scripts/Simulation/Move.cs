using TicTacShotgun.Players;

namespace TicTacShotgun.Simulation
{
    /// <summary>
    /// This class could be easily serialized if performed moves should be saved in a save file in future
    /// or transferred via network
    /// </summary>
    public class Move
    {
        public readonly Board.Index Index;
        public readonly Player Player;

        public Move(int x, int y, Player player)
        {
            Index = new Board.Index(x, y);
            Player = player;
        }
    }
}