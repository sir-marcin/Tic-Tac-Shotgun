using System.Diagnostics;
using TicTacShotgun.GameFlow;
using TMPro;
using UnityEngine;

namespace TicTacShotgun.GUI
{
    public class TurnTimeView : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI elapsedTimeLabel;

        const string TIME_FORMAT = @"mm\:ss";
        readonly Stopwatch stopwatch = new Stopwatch();
        
        void Awake()
        {
            GameController.OnGameStarted += OnGameStarted;
            GameController.OnGameEnded += OnGameEnded;
            enabled = false;
        }

        void OnDestroy()
        {
            GameController.OnGameStarted -= OnGameStarted;
            GameController.OnGameEnded -= OnGameEnded;
        }

        void OnGameStarted(GameController _)
        {
            stopwatch.Restart();
            enabled = true;
        }

        void OnGameEnded(GameEndDetails _)
        {
            stopwatch.Stop();
            enabled = false;
        }

        void Update()
        {
            elapsedTimeLabel.text = stopwatch.Elapsed.ToString(TIME_FORMAT);
        }
    }
}