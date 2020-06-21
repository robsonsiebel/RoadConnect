using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelCreatorData))]
public class LevelCreatorDataEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelCreatorData myScript = (LevelCreatorData)target;

        GUILayout.Space(20);

        if (GUILayout.Button("Clear Levels", GUILayout.Width(150)))
        {
            myScript.ClearLevels();
        }

        GUILayout.Space(20);

        if (GUILayout.Button("Clear Progress", GUILayout.Width(150)))
        {
            myScript.ClearProgress();
        }
    }
}