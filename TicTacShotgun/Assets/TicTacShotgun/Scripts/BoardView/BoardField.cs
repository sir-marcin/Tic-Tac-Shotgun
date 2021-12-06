using UnityEngine;

namespace TicTacShotgun.BoardView
{
    public class BoardField : MonoBehaviour
    {
        [SerializeField] SpriteRenderer playerMarker;

        public int X { get; private set; }
        public int Y { get; private set; }

        public void Initialize(int x, int y, Sprite initialSprite)
        {
            X = x;
            Y = y;
            
            SetSprite(initialSprite);
        }

        public void SetSprite(Sprite sprite)
        {
            playerMarker.sprite = sprite;
        }
    }
}