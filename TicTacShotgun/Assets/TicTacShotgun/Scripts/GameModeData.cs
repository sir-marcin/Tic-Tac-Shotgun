using System;

namespace TicTacShotgun
{
    public static class GameModeData
    {
        public static Action<GameMode> OnGameModeChanged = gm => { };
        public static GameMode SelectedGameMode { get; private set; }

        public static void SetGameMode(GameMode gameMode)
        {
            SelectedGameMode = gameMode;
        }
    }

    public enum GameMode
    {
        PlayerVsComputer = 0,
        PlayerVsPlayer = 1,
        ComputerVsComputer = 2
    }
}