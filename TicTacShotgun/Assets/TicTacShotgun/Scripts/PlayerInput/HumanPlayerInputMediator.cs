using System;
using TicTacShotgun.BoardView;
using TicTacShotgun.Simulation;
using UnityEngine;

namespace TicTacShotgun.PlayerInput
{
    public class HumanPlayerInputMediator : IDisposable
    {
        Board board;
        Player currentPlayer;
        
        public HumanPlayerInputMediator(Board board)
        {
            this.board = board;
            PlayerController.OnPlayerChanged += OnPlayerChanged;
            BoardMouseInput.OnClick += OnMouseClick;
        }

        void OnMouseClick(BoardField boardField)
        {
            if (currentPlayer is HumanLocalPlayer)
            {
                var move = new Move(boardField.X, boardField.Y, currentPlayer.Index);
                board.MakeMove(move);
                Debug.Log($"Made move by player {currentPlayer.Index}");
            }
        }

        public void Dispose()
        {
            PlayerController.OnPlayerChanged -= OnPlayerChanged;
            BoardMouseInput.OnClick += OnMouseClick;
        }

        void OnPlayerChanged(Player player)
        {
            currentPlayer = player;
        }
    }
}