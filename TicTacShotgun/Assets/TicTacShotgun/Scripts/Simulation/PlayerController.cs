using System.Collections.Generic;
using System.Linq;
using TicTacShotgun.Utils;
using UnityEngine;

namespace TicTacShotgun.Simulation
{
    public class PlayerController
    {
        const int P1 = 1;
        const int P2 = 2;

        Player player1;
        Player player2;
        readonly List<PlayerDetails> playerDetailsList;

        public PlayerDetails CurrentPlayerDetails => playerDetailsList[0];

        public PlayerController(VisualConfig visualConfig, Player player1, Player player2)
        {
            this.player1 = player1;
            this.player2 = player2;

            playerDetailsList = new List<PlayerDetails>
            {
                new PlayerDetails(player1, P1, visualConfig.MarkerP1),
                new PlayerDetails(player2, P2, visualConfig.MarkerP2)
            };
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
            var playerDetails = playerDetailsList.FirstOrDefault(p => p.Index == index);

            if (playerDetails == null)
            {
                TicTacLogger.LogError($"No player found for index {index}");
                return null;
            }

            return playerDetails;
        }
        
        public class PlayerDetails
        {
            public readonly Player Player;
            public readonly int Index;
            public readonly Sprite Sprite;

            public PlayerDetails(Player player, int index, Sprite sprite)
            {
                Player = player;
                Index = index;
                Sprite = sprite;
            }
        }
    }
}