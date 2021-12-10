using TicTacShotgun.BoardView;
using TicTacShotgun.GameFlow;
using TicTacShotgun.Players;
using TicTacShotgun.Simulation;
using UnityEngine;
using UnityEngine.UI;

namespace TicTacShotgun.GUI
{
    public class HintButtonController : MonoBehaviour
    {
        [SerializeField] Button hintButton;
        [SerializeField] BoardGridViewController gridViewController;

        TicTacToeBrain brain;
        Player currentPlayer;
        
        void Awake()
        {
            GameController.OnGameStarted += OnGameStarted;
            PlayerController.OnPlayerChanged += OnPlayerChanged;
            hintButton.onClick.AddListener(HandleHintButtonClick);
            
            gameObject.SetActive(false);
        }

        void OnDestroy()
        {
            GameController.OnGameStarted -= OnGameStarted;
            PlayerController.OnPlayerChanged -= OnPlayerChanged;
            
            if (hintButton != null)
            {
                hintButton.onClick.RemoveListener(HandleHintButtonClick);
            }
        }

        void OnPlayerChanged(Player player)
        {
            currentPlayer = player;
        }

        void OnGameStarted(GameController gameController)
        {
            if (gameController.CurrentGameMode != GameMode.PlayerVsComputer)
            {
                gameObject.SetActive(false);
                return;
            }
            else
            {
                gameObject.SetActive(true);
            }
            
            var board = gameController.CurrentGameInstance.Board;
            brain = new RandomMoveBrain(board);
        }
        
        void HandleHintButtonClick()
        {
            var nextBestMoveIndex = brain.GetNextMove(currentPlayer);
            gridViewController.ShowHint(nextBestMoveIndex);
        }
    }
}