using TicTacShotgun.GameFlow;
using TicTacShotgun.Players;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacShotgun.GUI
{
    public class UndoButtonController : MonoBehaviour
    {
        [SerializeField] Button undoButton;

        BoardHistoryController boardHistoryController;
        bool undoEnabled;
        
        void Awake()
        {
            GameController.OnGameStarted += OnGameStarted;
            GameController.OnGameEnded += OnGameEnded;
            PlayerController.OnPlayerChanged += OnPlayerChanged;
        }

        void OnDestroy()
        {
            GameController.OnGameStarted -= OnGameStarted;
            GameController.OnGameEnded -= OnGameEnded;
            PlayerController.OnPlayerChanged -= OnPlayerChanged;

            if (undoButton != null)
            {
                undoButton.onClick.RemoveListener(UndoLastMove);
            }
        }

        void OnPlayerChanged(Player player)
        {
            if (!undoEnabled)
            {
                return;
            }
            
            if (player is HumanLocalPlayer)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        void OnGameStarted(GameController gameController)
        {
            if (gameController.CurrentGameMode != GameMode.PlayerVsComputer)
            {
                undoEnabled = false;
                Hide();
                return;
            }

            undoEnabled = true;
            Show();

            boardHistoryController = gameController.BoardHistoryController;
            
            undoButton.onClick.AddListener(UndoLastMove);
        }

        void OnGameEnded(GameEndDetails _)
        {
            if (undoButton != null)
            {
                undoButton.onClick.RemoveListener(UndoLastMove);
            }
            
            Hide();
        }

        void UndoLastMove()
        {
            boardHistoryController.UndoLastMove();
        }

        void Hide()
        {
            gameObject.SetActive(false);
        }

        void Show()
        {
            gameObject.SetActive(true);
        }
    }
}