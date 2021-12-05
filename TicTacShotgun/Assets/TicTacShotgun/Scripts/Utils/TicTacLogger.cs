using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace TicTacShotgun.Utils
{
    public static class TicTacLogger
    {
        [Conditional("DEBUG")]
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        [Conditional("DEBUG")]
        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }
        
        [Conditional("DEBUG")]
        public static void LogError(string message)
        {
            Debug.LogError(message);
        }
    }
}