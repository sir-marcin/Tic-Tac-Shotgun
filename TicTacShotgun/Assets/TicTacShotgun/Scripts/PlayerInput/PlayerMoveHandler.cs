using System;
using TicTacShotgun.BoardView;
using TicTacShotgun.Simulation;
using TicTacShotgun.Utils;
using UnityEngine;

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
                currentPlayer.OnMovePerformed -= OnPlayerMovePerformed;
            }
        }

        void OnPlayerChanged(Player player)
        {
            if (currentPlayer != null)
            {
                currentPlayer.OnMovePerformed -= OnPlayerMovePerformed;
            }
            
            currentPlayer = player;
            currentPlayer.OnMovePerformed += OnPlayerMovePerformed;
        }

        void OnPlayerMovePerformed(Move move)
        {
            if (!board.IsMoveValid(move))
            {
                return;
            }
                
            board.MakeMove(move);
        }
    }
}