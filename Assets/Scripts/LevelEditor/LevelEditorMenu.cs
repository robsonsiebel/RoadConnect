using UnityEditor;
using UnityEngine;

public class LevelEditorMenu : MonoBehaviour
{
    [MenuItem("Level Editor/Launch")]
    static void OpenLevelEditor()
    {
        if (Application.isPlaying)
        {
            GameObject newGO = Resources.Load("Editor") as GameObject;
            Instantiate(newGO, Vector3.zero, Quaternion.identity);
        }
        else
        {
            EditorUtility.DisplayDialog("Level Editor", "You must be in play mode to use the Level Editor. \nFor changes please refer to the prefab in the Resources folder.", "Ok");
        }
        
    }

    [MenuItem("Level Editor/Edit Level Data")]
    static void OpenLevelData()
    {
        ScriptableObject asset = Resources.Load("LevelCreatorData") as ScriptableObject;
        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }

}