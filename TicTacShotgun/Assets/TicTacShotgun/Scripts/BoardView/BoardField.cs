using System;
using TicTacShotgun.PlayerInput;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TicTacShotgun.BoardView
{
    public class BoardField : MonoBehaviour, IClickable
    {
        [SerializeField] SpriteRenderer playerMarker;

        public event Action<BoardField> OnSelected = bf => { };

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

        public void Click()
        {
            OnSelected.Invoke(this);
        }
    }
}