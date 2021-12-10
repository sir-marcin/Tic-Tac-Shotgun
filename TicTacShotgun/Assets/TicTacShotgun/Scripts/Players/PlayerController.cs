using System;
using System.Collections.Generic;
using System.Linq;
using TicTacShotgun.Simulation;
using TicTacShotgun.Utils;
using UnityEngine;

namespace TicTacShotgun.Players
{
    public class PlayerController : IDisposable
    {
        public static event Action<Player> OnPlayerChanged = p => { };

        const int PLAYER1_INDEX = 1;
        const int PLAYER2_INDEX = 2;
        
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

                currentPlayer?.OnTurnEnd();
                
                currentPlayer = value;
                OnPlayerChanged.Invoke(currentPlayer);
                
                currentPlayer.OnTurnStart();
            }
        }
        public PlayerDetails CurrentPlayerDetails => GetPlayerDetails(currentPlayer);
        public PlayerDetails OtherPlayerDetails => currentPlayer == player1 
            ? GetPlayerDetails(player2) 
            : GetPlayerDetails(player1);

        public PlayerController(VisualConfig visualConfig, Board board, GameMode gameMode)
        {
            switch (gameMode)
            {
                case GameMode.PlayerVsComputer:
                    player1 = new HumanLocalPlayer(PLAYER1_INDEX, board);
                    player2 = new RandomMoveComputerPlayer(PLAYER2_INDEX, board);
                    break;
                case GameMode.PlayerVsPlayer:
                    player1 = new HumanLocalPlayer(PLAYER1_INDEX, board);
                    player2 = new HumanLocalPlayer(PLAYER2_INDEX, board);
                    break;
                case GameMode.ComputerVsComputer:
                    player1 = new RandomMoveComputerPlayer(PLAYER1_INDEX, board);
                    player2 = new RandomMoveComputerPlayer(PLAYER2_INDEX, board);
                    break;
                default:
                    TicTacLogger.LogError($"Unsupported game mode: {GameModeData.SelectedGameMode}");
                    break;
            }
            
            playerDetailsList = new List<PlayerDetails>
            {
                new PlayerDetails(player1, visualConfig?.MarkerP1),
                new PlayerDetails(player2, visualConfig?.MarkerP2)
            };

            CurrentPlayer = player1;
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

        public void ChangePlayer()
        {
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