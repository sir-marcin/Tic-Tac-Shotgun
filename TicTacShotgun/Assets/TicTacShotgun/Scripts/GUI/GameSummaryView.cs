using System;
using DG.Tweening;
using TicTacShotgun.GameFlow;
using TMPro;
using UnityEngine;

namespace TicTacShotgun.GUI
{
    public class GameSummaryView : MonoBehaviour
    {
        [SerializeField] CanvasGroup rootCanvasGroup;
        [SerializeField] TextMeshProUGUI summaryLabel;
        
        void Awake()
        {
            GameController.OnGameStarted += OnGameStarted;
            GameController.OnGameEnded += OnGameEnded;

        }

        void Start()
        {
            rootCanvasGroup.gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            GameController.OnGameStarted -= OnGameStarted;
            GameController.OnGameEnded -= OnGameEnded;
        }

        void OnGameStarted(GameController _)
        {
            Hide();
        }

        void OnGameEnded(GameEndDetails gameEndDetails)
        {
            rootCanvasGroup.gameObject.SetActive(true);
            rootCanvasGroup.alpha = 0f;
            rootCanvasGroup.DOFade(1f, .1f).SetEase(Ease.OutQuad);
            
            string summaryText = string.Empty;
            
            switch (gameEndDetails.BoardState)
            {
                case GameBoardState.Draw:
                    summaryText = "Draw";
                    break;
                case GameBoardState.Win:
                    summaryText = $"Player {gameEndDetails.Champion.Index} won!";
                    break;
            }

            summaryLabel.text = summaryText;
        }

        void Hide()
        {
            rootCanvasGroup.DOFade(0f, .1f)
                .OnComplete(() => { rootCanvasGroup.gameObject.SetActive(false); });
        }
    }
}