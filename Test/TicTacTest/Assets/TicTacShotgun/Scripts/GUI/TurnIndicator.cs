using TicTacShotgun.Players;
using TMPro;
using UnityEngine;

namespace TicTacShotgun.GUI
{
    public class TurnIndicator : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI turnIndicatorLabel;

        void Awake()
        {
            PlayerController.OnPlayerChanged += OnPlayerChanged;
        }

        void OnDestroy()
        {
            PlayerController.OnPlayerChanged -= OnPlayerChanged;
        }

        void OnPlayerChanged(Player player)
        {
            turnIndicatorLabel.text = $"Player {player.Index}";
        }
    }
}