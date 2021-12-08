using TicTacShotgun.GameFlow;
using UnityEngine;

namespace TicTacShotgun.BoardView
{
    public class BoardViewScaler : MonoBehaviour
    {
        [SerializeField] SpriteRenderer backgroundRenderer;
        [SerializeField] GameObject grid;
        [SerializeField, Range(0f, 1f)] float gridMargin = .33f;
        [SerializeField] bool realtimeScaling = true;
        
        Camera mainCamera;
        Bounds gameGridBounds;
        
        void Awake()
        {
            mainCamera = Camera.main;
            CalculateSpriteBounds(grid);
            
            GameController.OnGameStarted += OnGameStarted;
        }

        void OnDestroy()
        {
            GameController.OnGameStarted -= OnGameStarted;
        }

        void Update()
        {
            if (!realtimeScaling)
            {
                return;
            }
            
            ScaleCameraToFitGameView();
            ScaleGridToFitCamera();
        }

        void OnGameStarted(GameController gameController)
        {
            ScaleCameraToFitGameView();
            ScaleGridToFitCamera();
        }

        [ContextMenu("Scale Camera To Fit Game View")]
        void ScaleCameraToFitGameView()
        {
            if (mainCamera.aspect > 1f)
            {
                // horizontal
                var bgWidth = backgroundRenderer.bounds.size.y;
                mainCamera.orthographicSize = bgWidth * backgroundRenderer.transform.localScale.x / 2f / mainCamera.aspect;
            }
            else
            {
                // vertical
                var bgHeight = backgroundRenderer.bounds.size.y;
                mainCamera.orthographicSize = bgHeight * backgroundRenderer.transform.localScale.y / 2f;
            }
        }

        [ContextMenu("Scale Grid To Fit Camera")]
        void ScaleGridToFitCamera()
        {
            var gridSize = gameGridBounds.size.x;
            
            var worldScreenHeight = mainCamera.orthographicSize * 2f * (1f - gridMargin);
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            if (mainCamera.aspect > 1f)
            {
                // horizontal
                grid.transform.localScale = Vector3.one * worldScreenHeight / gridSize;
            }
            else
            {
                // vertical
                grid.transform.localScale = Vector3.one * worldScreenWidth / gridSize;
            }
        }

        void CalculateSpriteBounds(GameObject go)
        {
            var childSprites = go.GetComponentsInChildren<SpriteRenderer>();

            for (int i = 0; i < childSprites.Length; i++)
            {
                gameGridBounds.Encapsulate(childSprites[i].sprite.bounds);
            }
        }
    }
}