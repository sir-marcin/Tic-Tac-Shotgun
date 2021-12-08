using System;
using System.Linq;
using TicTacShotgun.GameFlow;
using TicTacShotgun.Players;
using TicTacShotgun.Simulation;
using TicTacShotgun.Utils;
using UnityEngine;

namespace TicTacShotgun.BoardView
{
    public class BoardGridViewController : MonoBehaviour
    {
        [SerializeField] SpriteRenderer backgroundRenderer;
        [SerializeField] SpriteRenderer[] fieldSeparators;
        [SerializeField] BoardField[] fields;

        PlayerController playerController;
        
        void Awake()
        {
            GameController.OnGameStarted += OnGameStarted;
        }

        void OnDestroy()
        {
            GameController.OnGameStarted -= OnGameStarted;
        }

        void OnGameStarted(GameController gameController)
        {
            var visualConfig = gameController.VisualConfig;
            var board = gameController.CurrentGameInstance.Board;

            board.OnMovePerformed += OnMovePerformed;
            board.OnUndoMovePerformed += OnUndoMovePerformed;
            
            backgroundRenderer.sprite = visualConfig.Background;

            for (int i = 0; i < fieldSeparators.Length; i++)
            {
                fieldSeparators[i].sprite = visualConfig.FieldSeparator;
            }

            playerController = gameController.PlayerController;
            var boardArray = board.GetCurrentBoardArray();
            var boardSize = Board.BOARD_SIZE;
            var boardFieldIndex = 0;
            
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    var playerSprite = boardArray[x, y] == Board.EMPTY_FIELD
                        ? null
                        : playerController.GetPlayerDetails(boardArray[x, y]).Sprite;
                    
                    fields[boardFieldIndex].Initialize(x, y, playerSprite);

                    boardFieldIndex++;
                }   
            }
        }

        void OnUndoMovePerformed(Move move)
        {
            SetFieldSprite(move.Index, null);
        }

        void OnMovePerformed(Move move)
        {
            SetFieldSprite(move.Index, playerController.GetPlayerDetails(move.Player).Sprite);
        }

        void SetFieldSprite(Board.Index boardIndex, Sprite sprite)
        {
            var field = fields.FirstOrDefault(f => f.Index.Equals(boardIndex));
            
            field?.SetSprite(sprite);
        }
    }
}
