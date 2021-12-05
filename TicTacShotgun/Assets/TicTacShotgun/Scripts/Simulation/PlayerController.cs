using System;
using System.Collections.Generic;
using System.Linq;
using TicTacShotgun.Utils;
using UnityEngine;

namespace TicTacShotgun.Simulation
{
    public class PlayerController : IDisposable
    {
        public static event Action<Player> OnPlayerChanged = p => { };

        Player player1;
        Player player2;
        Player currentPlayer;
        readonly List<PlayerDetails> playerDetailsList;

        Player CurrentPlayer
        {
            get => currentPlayer;
            set
            {
                if (value == currentPlayer)
                {
                    return;
                }

                currentPlayer = value;
                OnPlayerChanged.Invoke(currentPlayer);
            }
        }
        public PlayerDetails CurrentPlayerDetails => GetPlayerDetails(currentPlayer);
        public PlayerDetails OtherPlayerDetails => currentPlayer == player1 
            ? GetPlayerDetails(player2) 
            : GetPlayerDetails(player1);

        public PlayerController(VisualConfig visualConfig, Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            playerDetailsList = new List<PlayerDetails>
            {
                new PlayerDetails(player1, visualConfig.MarkerP1),
                new PlayerDetails(player2, visualConfig.MarkerP2)
            };

            CurrentPlayer = player1;

            player1.OnMovePerformed += OnPlayerMovePerformed;
            player2.OnMovePerformed += OnPlayerMovePerformed;
        }

        public PlayerDetails GetPlayerDetails(Player player)
        {
            var playerDetails = playerDetailsList.FirstOrDefault(p => p.Player == player);

            if (playerDetails == null)
            {
                TicTacLogger.LogError("No index found for player");
                return null;
            }

            return playerDetails;
        }

        public PlayerDetails GetPlayerDetails(int index)
        {
            var playerDetails = playerDetailsList.FirstOrDefault(p => p.Player.Index == index);

            if (playerDetails == null)
            {
                TicTacLogger.LogError($"No player found for index {index}");
                return null;
            }

            return playerDetails;
        }

        void OnPlayerMovePerformed(Move move)
        {
            if (move.PlayerIndex != currentPlayer.Index)
            {
                return;
            }
            
            CurrentPlayer = OtherPlayerDetails.Player;
        }
        
        public class PlayerDetails
        {
            public readonly Player Player;
            public readonly Sprite Sprite;

            public PlayerDetails(Player player, Sprite sprite)
            {
                Player = player;
                Sprite = sprite;
            }
        }

        public void Dispose()
        {
            player1.Dispose();
            player2.Dispose();
        }
    }
}