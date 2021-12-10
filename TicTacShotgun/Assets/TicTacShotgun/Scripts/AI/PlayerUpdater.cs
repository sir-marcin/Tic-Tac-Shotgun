using TicTacShotgun.Players;
using UnityEngine;

namespace TicTacShotgun.Simulation
{
    public class PlayerUpdater : MonoBehaviour
    {
        Player currentPlayer;
        
        void Awake()
        {
            PlayerController.OnPlayerChanged += HandlePlayerChanged;
        }

        void OnDestroy()
        {
            PlayerController.OnPlayerChanged -= HandlePlayerChanged;
        }

        void Update()
        {
            currentPlayer?.Update();
        }

        void HandlePlayerChanged(Player player)
        {
            currentPlayer = player;
        }
    }
}