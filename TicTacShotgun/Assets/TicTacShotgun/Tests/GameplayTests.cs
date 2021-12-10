using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using TicTacShotgun;
using TicTacShotgun.GameFlow;
using TicTacShotgun.Players;
using TicTacShotgun.Simulation;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class GameplayTests
    {
        Board board;
        Player player1;
        Player player2;
        HintsController hintsController;
        GameController gameController;

        readonly List<Board.Index> winningMoves = new List<Board.Index>
        {
            new Board.Index(0, 0),
            new Board.Index(1, 0),
            new Board.Index(2, 0)
        };
        
        readonly List<Board.Index> losingMoves = new List<Board.Index>
        {
            new Board.Index(2, 2),
            new Board.Index(0, 2),
            new Board.Index(1, 1)
        };
        
        [OneTimeSetUp]
        public void Setup()
        {
            GameModeData.SetGameMode(GameMode.PlayerVsPlayer);
            gameController = new GameObject().AddComponent<GameController>();
        }

        [Test]
        public void HintTest()
        {
            RestartSimulation();
            
            MakeRandomBoardMove(player1);
            MakeRandomBoardMove(player2);
            MakeRandomBoardMove(player1);
            MakeRandomBoardMove(player2);

            var hintedIndex = hintsController.GetNextHint(player1);
            var hintedMove = new Move(hintedIndex.X, hintedIndex.Y, player1);
            
            Assert.That(board.IsMoveValid(hintedMove));
        }

        [Test]
        public void UndoTest()
        {
            RestartSimulation();
            
            MakeRandomBoardMove(player1);
            MakeRandomBoardMove(player2);
            MakeRandomBoardMove(player1);
            MakeRandomBoardMove(player2);

            var randomMove = GetRandomAvailableMove(player1);
            
            board.MakeMove(randomMove);
            board.UndoMove(randomMove);
            
            // that move we just did and undid should be valid again 
            Assert.That(board.IsMoveValid(randomMove));
        }

        [Test]
        public void WinTest()
        {
            RestartSimulation();
            
            gameController.CurrentGameInstance.OnGameFinished += details =>
            {
                Assert.That(details.Champion == player1);
            };
            
            var playerController = gameController.PlayerController; 
            
            for (int i = 0; i < 3; i++)
            {
                board.MakeMove(new Move(winningMoves[i], playerController.CurrentPlayerDetails.Player));

                if (gameController.CurrentGameInstance.CurrentGameState != GameBoardState.GameInProgress)
                {
                    return;
                }
                
                board.MakeMove(new Move(losingMoves[i], playerController.CurrentPlayerDetails.Player));
                
                if (gameController.CurrentGameInstance.CurrentGameState != GameBoardState.GameInProgress)
                {
                    return;
                }
            }
            
            // game should end by this point and not reach this line
            Assert.Fail();
        }

        [Test]
        public void LooseTest()
        {
            RestartSimulation();

            gameController.CurrentGameInstance.OnGameFinished += details =>
            {
                Assert.That(details.Champion == player2);
            };
            
            var playerController = gameController.PlayerController;
            
            for (int i = 0; i < 3; i++)
            {
                board.MakeMove(new Move(losingMoves[i], playerController.CurrentPlayerDetails.Player));
                
                if (gameController.CurrentGameInstance.CurrentGameState != GameBoardState.GameInProgress)
                {
                    return;
                }
                
                board.MakeMove(new Move(winningMoves[i], playerController.CurrentPlayerDetails.Player));
                
                if (gameController.CurrentGameInstance.CurrentGameState != GameBoardState.GameInProgress)
                {
                    return;
                }
            }
            
            // game should end by this point and not reach this line
            Assert.Fail();
        }

        void RestartSimulation()
        {
            gameController.Restart();
            board = gameController.CurrentGameInstance.Board;
            player1 = gameController.PlayerController.GetPlayerDetails(1).Player;
            player2 = gameController.PlayerController.GetPlayerDetails(2).Player;
            hintsController = gameController.HintsController;
        }
        
        Move GetRandomAvailableMove(Player player)
        {
            var randomIndex = board.GetRandomUnoccupiedIndex();
            return new Move(randomIndex, player);
        }
        
        void MakeRandomBoardMove(Player player)
        {
            var move = GetRandomAvailableMove(player);
            board.MakeMove(move);
        }
    }
}
