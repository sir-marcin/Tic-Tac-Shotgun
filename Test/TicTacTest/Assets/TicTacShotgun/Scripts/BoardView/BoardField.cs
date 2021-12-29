using DG.Tweening;
using TicTacShotgun.Simulation;
using UnityEngine;

namespace TicTacShotgun.BoardView
{
    public class BoardField : MonoBehaviour
    {
        [SerializeField] SpriteRenderer playerMarker;

        Color defaultColor = Color.white;
        
        public Board.Index Index { get; private set; }
        public Sprite Sprite => playerMarker.sprite;

        public void Initialize(int x, int y, Sprite initialSprite)
        {
            defaultColor.a = 0f;
            Index = new Board.Index(x, y);
            
            SetSprite(initialSprite);
        }

        public void SetSprite(Sprite sprite, bool animate = true)
        {
            if (animate && sprite == null)
            {
                playerMarker.DOFade(0f, .1f).OnComplete(() => playerMarker.sprite = null);
            }
            else
            {
                playerMarker.color = defaultColor;
                playerMarker.sprite = sprite;
                playerMarker.DOFade(1f, .1f);
            }
        }
    }
}