using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacShotgun.GUI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField] Button startButton;
        [SerializeField] SceneLoader sceneLoader;
        
        GameModeButton[] gameModeButtons;
        
        void Awake()
        {
            gameModeButtons = GetComponentsInChildren<GameModeButton>();

            for (int i = 0; i < gameModeButtons.Length; i++)
            {
                gameModeButtons[i].OnButtonPressed += HandleGameModeButtonPressed;
            }
            
            startButton.onClick.AddListener(HandleStartButtonClick);
            startButton.gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            if (startButton != null)
            {
                startButton.onClick.RemoveListener(HandleStartButtonClick);
            }
            
            for (int i = 0; i < gameModeButtons.Length; i++)
            {
                if (gameModeButtons[i] != null)
                {
                    gameModeButtons[i].OnButtonPressed -= HandleGameModeButtonPressed;
                }
            }
        }
        
        void HandleStartButtonClick()
        {
            sceneLoader.LoadGameplayScene();
        }

        void HandleGameModeButtonPressed(GameMode gameMode)
        {
            GameModeData.SetGameMode(gameMode);

            if (!startButton.gameObject.activeSelf)
            {
                startButton.gameObject.SetActive(true);
                
                var startButtonCanvasGroup = startButton.GetComponent<CanvasGroup>();
                
                if (startButtonCanvasGroup != null)
                {
                    startButtonCanvasGroup.alpha = 0f;
                    startButtonCanvasGroup.DOFade(1f, .1f);
                }
            }
        }
    }
}