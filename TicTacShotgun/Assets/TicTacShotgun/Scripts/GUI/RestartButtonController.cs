using TicTacShotgun.GameFlow;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacShotgun.GUI
{
    public class RestartButtonController : MonoBehaviour
    {
        [SerializeField] Button restartButton;

        GameController gameController;
        
        void Awake()
        {
            GameController.OnGameStarted += OnGameStarted;
            restartButton.onClick.AddListener(Restart);
        }

        void OnDestroy()
        {
            GameController.OnGameStarted -= OnGameStarted;
            restartButton.onClick.RemoveListener(Restart);
        }

        void OnGameStarted(GameController gameController)
        {
            this.gameController = gameController;
        }

        void Restart()
        {
            gameController.Restart();
        }
    }
}