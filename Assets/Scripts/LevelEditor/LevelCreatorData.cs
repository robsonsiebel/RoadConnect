using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "Level Creator Data")]
public class LevelCreatorData : ScriptableObject
{
    [SerializeField]
    public Sprite[] LevelSprites;

    public List<LevelData> GameLevels = new List<LevelData>();

    public void AddNewNevel(LevelData newLevel)
    {
        newLevel.LevelID = GameLevels.Count;
        GameLevels.Add(newLevel);
        EditorUtility.SetDirty(this);
        Debug.Log("level added");
    }
    
    public void ClearLevels()
    {
        GameLevels.Clear();
    }

    public void ClearProgress()
    {
        PlayerPrefs.DeleteAll();
    }
}
