using System;
using TicTacShotgun.Players;
using TicTacShotgun.Simulation;

namespace TicTacShotgun.GameFlow
{
    public class TurnController : IDisposable
    {
        readonly PlayerController playerController;
        readonly TicTacToeGame game;
        
        public TurnController(PlayerController playerController, TicTacToeGame game)
        {
            this.playerController = playerController;
            this.game = game;
            
            game.OnNextMoveAvailable += OnNextMoveAvailable;
        }

        public void Dispose()
        {
            game.OnNextMoveAvailable -= OnNextMoveAvailable;
        }

        void OnNextMoveAvailable()
        {
            playerController.ChangePlayer();
        }
    }
}
