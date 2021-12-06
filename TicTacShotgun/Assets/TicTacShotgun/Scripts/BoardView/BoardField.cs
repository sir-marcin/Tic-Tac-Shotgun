using TicTacShotgun.Simulation;
using UnityEngine;

namespace TicTacShotgun.BoardView
{
    public class BoardField : MonoBehaviour
    {
        [SerializeField] SpriteRenderer playerMarker;

        public Board.Index Index { get; private set; }

        public void Initialize(int x, int y, Sprite initialSprite)
        {
            Index = new Board.Index(x, y);
            
            SetSprite(initialSprite);
        }

        public void SetSprite(Sprite sprite)
        {
            playerMarker.sprite = sprite;
        }
    }
}