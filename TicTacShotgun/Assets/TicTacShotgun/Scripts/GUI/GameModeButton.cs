using System;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacShotgun.GUI
{
    public class GameModeButton : MonoBehaviour
    {
        [SerializeField] GameMode gameMode;
        [SerializeField] Button button;

        public event Action<GameMode> OnButtonPressed = gm => { };

        void Awake()
        {
            button.onClick.AddListener(HandleButtonClick);
        }

        void OnDestroy()
        {
            button.onClick.RemoveListener(HandleButtonClick);
        }

        void HandleButtonClick()
        {
            OnButtonPressed.Invoke(gameMode);
        }
    }
}