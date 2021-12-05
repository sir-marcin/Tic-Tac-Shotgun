using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TicTacShotgun.BoardView
{
    [RequireComponent(typeof(EventTrigger))]
    public class BoardField : MonoBehaviour
    {
        [SerializeField] SpriteRenderer playerMarker;

        public event Action<BoardField> OnSelected = bf => { };
        public event Action<BoardField> OnPointerEntered = bf => { };
        public event Action<BoardField> OnPointerLeft = bf => { };

        public int X { get; private set; }
        public int Y { get; private set; }

        void Awake()
        {
            var eventTrigger = GetComponent<EventTrigger>();

            var onSelectEntry = new EventTrigger.Entry();
            onSelectEntry.eventID = EventTriggerType.Select;
            onSelectEntry.callback.AddListener(data => OnSelected.Invoke(this));

            var onPointerEnterEntry = new EventTrigger.Entry();
            onPointerEnterEntry.eventID = EventTriggerType.PointerEnter;
            onPointerEnterEntry.callback.AddListener(data => OnPointerEntered.Invoke(this));
            
            var onPointerExitEntry = new EventTrigger.Entry();
            onPointerExitEntry.eventID = EventTriggerType.PointerExit;
            onPointerExitEntry.callback.AddListener(data => OnPointerLeft.Invoke(this));
            
            eventTrigger.triggers.Add(onSelectEntry);
            eventTrigger.triggers.Add(onPointerEnterEntry);
            eventTrigger.triggers.Add(onPointerExitEntry);
        }

        public void Initialize(int x, int y, Sprite initialSprite)
        {
            X = x;
            Y = y;
            
            //SetSprite(initialSprite);
        }

        public void SetSprite(Sprite sprite)
        {
            playerMarker.sprite = sprite;
        }
    }
}