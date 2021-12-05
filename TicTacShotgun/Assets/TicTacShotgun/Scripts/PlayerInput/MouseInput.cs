using UnityEngine;

namespace TicTacShotgun.PlayerInput
{
    public class MouseInput : MonoBehaviour
    {
        Camera mainCamera;
        readonly RaycastHit2D[] hitCache = new RaycastHit2D[1]; 
        
        void Awake()
        {
            mainCamera = Camera.main;
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

            if (!hitCache[0].collider.TryGetComponent<IClickable>(out var clickable))
            {
                return;
            }

            clickable.Click();
        }
    }
}