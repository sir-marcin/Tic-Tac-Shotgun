using System;
using TicTacShotgun.Players;
using TicTacShotgun.Simulation;

namespace TicTacShotgun.PlayerInput
{
    public class PlayerMoveHandler : IDisposable
    {
        readonly Board board;
        Player currentPlayer;
        
        public PlayerMoveHandler(Board board)
        {
            this.board = board;
            PlayerController.OnPlayerChanged += OnPlayerChanged;
        }

        public void Dispose()
        {
            PlayerController.OnPlayerChanged -= OnPlayerChanged;
            
            if (currentPlayer != null)
            {
                currentPlayer.OnMoveRequested -= OnPlayerMoveRequested;
            }
        }

        void OnPlayerChanged(Player player)
        {
            if (currentPlayer != null)
            {
                currentPlayer.OnMoveRequested -= OnPlayerMoveRequested;
            }
            
            currentPlayer = player;
            currentPlayer.OnMoveRequested += OnPlayerMoveRequested;
        }

        void OnPlayerMoveRequested(Move move)
        {
            if (!board.IsMoveValid(move))
            {
                return;
            }
            
            currentPlayer.OnMoveRequested -= OnPlayerMoveRequested;
            board.MakeMove(move);
        }
    }
}