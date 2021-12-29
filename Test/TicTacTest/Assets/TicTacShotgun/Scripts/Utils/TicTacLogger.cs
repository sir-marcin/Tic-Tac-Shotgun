using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace TicTacShotgun.Utils
{
    public static class TicTacLogger
    {
        [Conditional("LOG")]
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        [Conditional("LOG")]
        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }
        
        [Conditional("LOG")]
        public static void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}