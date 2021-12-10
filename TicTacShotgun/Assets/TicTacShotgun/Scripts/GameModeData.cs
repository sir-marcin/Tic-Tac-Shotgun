using System;

namespace TicTacShotgun
{
    public static class GameModeData
    {
        public static Action<GameMode> OnGameModeChanged = gm => { };
        public static GameMode GameMode { get; private set; }

        public static void SetGameMode(GameMode gameMode)
        {
            GameMode = gameMode;
        }
    }

    public enum GameMode
    {
        PlayerVsComputer = 0,
        PlayerVsPlayer = 1,
        ComputerVsComputer = 2
    }
}