﻿using System;
using TicTacShotgun.GameFlow;
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

            backgroundRenderer.sprite = visualConfig.Background;

            for (int i = 0; i < fieldSeparators.Length; i++)
            {
                fieldSeparators[i].sprite = visualConfig.FieldSeparator;
            }

            playerController = gameController.PlayerController;
            var boardArray = gameController.CurrentGameInstance.Board.GetCurrentBoardArray();
            var boardSize = gameController.CurrentGameInstance.Board.BOARD_SIZE;
            var boardFieldIndex = 0;
            
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    var playerSprite = boardArray[x, y] == Board.EMPTY_FIELD
                        ? null
                        : playerController.GetPlayerDetails(boardArray[x, y]).Sprite;
                    
                    fields[boardFieldIndex].Initialize(x, y, playerSprite);
                    fields[boardFieldIndex].OnSelected += OnFieldSelected;

                    boardFieldIndex++;
                }   
            }
        }
        
        void OnFieldSelected(BoardField boardField)
        {
            TicTacLogger.Log($"Selected ({boardField.X},{boardField.Y})");
            
            boardField.SetSprite(playerController.CurrentPlayerDetails.Sprite);
        }
    }
}
