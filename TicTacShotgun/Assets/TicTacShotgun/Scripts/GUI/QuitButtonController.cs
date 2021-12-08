using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TicTacShotgun.GUI
{
    public class QuitButtonController : MonoBehaviour
    {
        [SerializeField] Button quitButton;

        void Awake()
        {
            quitButton.onClick.AddListener(Quit);
        }

        void OnDestroy()
        {
            quitButton.onClick.RemoveListener(Quit);
        }

        void Quit()
        {
            Application.Quit();
        }
    }
}