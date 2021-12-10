using UnityEngine;

namespace TicTacShotgun
{
    [CreateAssetMenu(fileName = "VisualConfig", menuName = "TTS/VisualConfig")]
    public class VisualConfig : ScriptableObject
    {
        [SerializeField] Sprite background;
        [SerializeField] Sprite markerP1;
        [SerializeField] Sprite markerP2;
        [SerializeField] Sprite fieldSeparator;
        [SerializeField] Sprite hintSprite;

        public Sprite Background => background;
        public Sprite MarkerP1 => markerP1;
        public Sprite MarkerP2 => markerP2;
        public Sprite FieldSeparator => fieldSeparator;
        public Sprite HintSprite => hintSprite;
    }
}