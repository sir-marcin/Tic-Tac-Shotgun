using DG.Tweening;
using UnityEngine;

namespace TicTacShotgun
{
    public class GameplayScreenFade : MonoBehaviour
    {
        [SerializeField, Min(0f)] float fadeInDuration = .5f;
        
        void Awake()
        {
            var canvasGroup = GetComponent<CanvasGroup>();

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.DOFade(0f, fadeInDuration).OnComplete(() =>
                {
                    gameObject.SetActive(false);
                });
            }
        }
    }
}