namespace TicTacShotgun
{
    public class PlayerIndexFactory
    {
        int currentIndex;
        
        public PlayerIndexFactory()
        {
            currentIndex = 1;
        }

        public int GetNextIndex()
        {
            return currentIndex++;
        }
    }
}