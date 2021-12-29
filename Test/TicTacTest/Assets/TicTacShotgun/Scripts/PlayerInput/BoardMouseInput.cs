using System;
using TicTacShotgun.BoardView;
using TicTacShotgun.Players;
using TicTacShotgun.Simulation;
using UnityEngine;

namespace TicTacShotgun.PlayerInput
{
    public class BoardMouseInput : MonoBehaviour
    {
        public static event Action<BoardField> OnClick = c => { };

        Camera mainCamera;
        bool inputEnabled;
        readonly RaycastHit2D[] hitCache = new RaycastHit2D[1]; 
        
        void Awake()
        {
            mainCamera = Camera.main;
            PlayerController.OnPlayerChanged += OnPlayerChanged;
        }

        void OnDestroy()
        {
            PlayerController.OnPlayerChanged -= OnPlayerChanged;
        }

        void OnPlayerChanged(Player player)
        {
            inputEnabled = player is HumanLocalPlayer;
        }

        void Update()
        {
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
            
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            if (Physics2D.RaycastNonAlloc(mousePosition, Vector2.zero, hitCache) == 0)
            {
                return;
            }

            if (!hitCache[0].collider.TryGetComponent<BoardField>(out var boardField))
            {
                return;
            }

            OnClick.Invoke(boardField);
        }
    }
}