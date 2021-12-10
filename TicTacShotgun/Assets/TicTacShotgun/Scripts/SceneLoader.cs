using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacShotgun
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] CanvasGroup screenFade;
        
        const int MAIN_MENU_SCENE_INDEX = 0;
        const int GAMEPLAY_SCENE_INDEX = 1;

        void Awake()
        {
            screenFade.gameObject.SetActive(false);
        }

        public void LoadGameplayScene()
        {
            screenFade.gameObject.SetActive(true);
            screenFade.alpha = 0f;
            screenFade.DOFade(1f, .2f).OnComplete(() =>
            {
                SceneManager.LoadScene(GAMEPLAY_SCENE_INDEX);
            });
        }
        
        public static void LoadMainMenuScene()
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE_INDEX);
        }
    }
}