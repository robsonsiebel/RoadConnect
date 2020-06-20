using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public LevelCreatorData LevelCreatorData;
    public LevelSelectMenu LevelSelect;
    public PuzzleManager PuzzleManager;
    public GameUI GameUI;


    private void Awake()
    {
        GameUI.BtnMenu.onClick.AddListener(OnMenuPressed);
    }

    void Start()
    {
        PopulateLevelSelect();
        LevelSelect.gameObject.SetActive(true);
    }

    private void OnEnable()
    {
        LevelSelect.OnLevelPressed += LoadLevel;
    }

    private void OnDisable()
    {
        LevelSelect.OnLevelPressed -= LoadLevel;
    }

    private void OnMenuPressed()
    {
        LevelSelect.gameObject.SetActive(true);
        PuzzleManager.ClearLevel();
    }

    private void LoadLevel(string levelID, string displayName)
    {
        LevelSelect.gameObject.SetActive(false);
        PuzzleManager.PopulateLevel(GetLevelWithID(levelID), LevelCreatorData.LevelSprites);
        GameUI.SetLevelName(displayName);
    }

    LevelData GetLevelWithID(string levelID)
    {
        LevelData level = new LevelData();

        foreach (LevelData levelData in LevelCreatorData.GameLevels)
        {
            if (levelData.levelID == levelID)
            {
                level = levelData;
            }
        }

        return level;
    }

    void PopulateLevelSelect()
    {
        foreach(LevelData level in LevelCreatorData.GameLevels)
        {
            LevelSelect.AddLevel(level.levelID);
        }

    }
}
