using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TicTacShotgun
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] CanvasGroup screenFade;
        
        const string MAIN_MENU_SCENE_NAME = "MainMenu";
        const string GAMEPLAY_SCENE_NAME = "Gameplay";

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
                SceneManager.LoadScene(GAMEPLAY_SCENE_NAME);
            });
        }
        
        public static void LoadMainMenuScene()
        {
            SceneManager.LoadScene(MAIN_MENU_SCENE_NAME);
        }
    }
}