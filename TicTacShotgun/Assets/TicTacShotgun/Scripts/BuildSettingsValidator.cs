using UnityEditor;

namespace TicTacShotgun
{
    [InitializeOnLoad]
    class BuildSettingsValidator
    {
        static BuildSettingsValidator()
        {
            if (EditorBuildSettings.scenes.Length != 2)
            {
                EditorUtility.DisplayDialog("Assign Scenes",
                    "It seems that there are no scenes assigned in Build Settings. Before running the game make sure to add scenes from \"Assets/TicTacShotgun/Scenes\" to Build Settings", "OK!");
            }
        }
    }
}