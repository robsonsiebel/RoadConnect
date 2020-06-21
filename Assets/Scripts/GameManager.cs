using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private int m_NumberOfLevels;
    private int m_CurrentLevel;

    public LevelCreatorData LevelCreatorData;
    public LevelSelectMenu LevelSelect;
    public PuzzleManager PuzzleManager;
    public GameUI GameUI;


    void Awake()
    {
        GameUI.BtnMenu.onClick.AddListener(OnMenuPressed);
        PuzzleManager.OnLevelComplete += HandleLevelComplete;
    }

    void Start()
    {
        m_NumberOfLevels = LevelCreatorData.GameLevels.Count;
        PopulateLevelSelect();
        LevelSelect.gameObject.SetActive(true);

    }

    private void HandleLevelComplete()
    {
        GameUI.LevelCompleteAnimation();

        if (!(m_CurrentLevel >= m_NumberOfLevels - 1))
        {
            StartCoroutine(LevelTransition());
        }
        else
        {
            StartCoroutine(EndGameAnimation());
        }
        
    }

    IEnumerator EndGameAnimation()
    {
        yield return new WaitForSeconds(1.5f);

        GameUI.AllLevelsCompleteAnimation();
    }

    IEnumerator LevelTransition()
    {
        yield return new WaitForSeconds(1.5f);

        GameUI.NewLevelAnimation();
        m_CurrentLevel++;
        LoadLevel(m_CurrentLevel);
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

    private void LoadLevel(int levelID)
    {
        m_CurrentLevel = levelID;
        LevelSelect.gameObject.SetActive(false);
        PuzzleManager.PopulateLevel(GetLevelWithID(levelID), LevelCreatorData.LevelSprites);
        GameUI.SetLevelName("Level " + levelID);
    }

    LevelData GetLevelWithID(int levelID)
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
