using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Level Creator Data")]
public class LevelCreatorData : ScriptableObject
{
    [SerializeField]
    public Sprite[] LevelSprites;

    public List<LevelData> GameLevels = new List<LevelData>();

    #region Public
    public void AddNewNevel(LevelData newLevel)
    {
#if UNITY_EDITOR
        newLevel.LevelID = GameLevels.Count;
        GameLevels.Add(newLevel);
        EditorUtility.SetDirty(this);
#endif
    }
    
    public void ReplaceLevel(int levelID, LevelData newLevelData)
    {
        for (int i = 0; i < GameLevels.Count; i++)
        {
            if (GameLevels[i].LevelID == levelID)
            {
                GameLevels[i] = newLevelData;
            }
        }
    }
    
    public void ClearLevels()
    {
        GameLevels.Clear();
        ClearProgress();
    }

    public void ClearProgress()
    {
        PlayerPrefs.DeleteAll();
    }
    #endregion
}
