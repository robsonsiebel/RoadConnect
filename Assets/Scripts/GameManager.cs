﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private readonly string LEVEL_KEY = "Level";

    private int m_NumberOfLevels;
    private int m_CurrentLevel;

    public LevelCreatorData LevelCreatorData;
    public PuzzleManager PuzzleManager;
    public GameUI GameUI;

    [Header("Screens")]
    public LevelSelectMenu LevelSelect;
    public TitleScreen TitleScreen;

    void Awake()
    {
        GameUI.BtnMenu.onClick.AddListener(OnMenuPressed);
        GameUI.BtnPlay.onClick.AddListener(OnPlayPressed);
        PuzzleManager.OnLevelComplete += HandleLevelComplete;
        TitleScreen.OnAnimationComplete += HandleTItleScreenAnimationComplete;
    }

    private void HandleTItleScreenAnimationComplete()
    {
        GameUI.ButtonPlayAppear();
    }

    void Start()
    {
        m_NumberOfLevels = LevelCreatorData.GameLevels.Count;
        PopulateLevelSelect();
        
        SoundLibrary.Instance.PlayMusic();

    }

    private void HandleLevelComplete()
    {
        GameUI.LevelCompleteAnimation();
        SaveProgress(m_CurrentLevel);

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
        PuzzleManager.OnLevelComplete -= HandleLevelComplete;
        TitleScreen.OnAnimationComplete -= HandleTItleScreenAnimationComplete;
    }

    private void OnMenuPressed()
    {
        LevelSelect.gameObject.SetActive(true);
        PuzzleManager.ClearLevel();
        PopulateLevelSelect();

        SoundLibrary.Instance.PlaySound(SFX.DefaultClick);
    }

    private void OnPlayPressed()
    {
        LeanTween.scale(GameUI.BtnPlay.gameObject, new Vector3(1.1f, 1.1f, 1), 0.5f).setEasePunch();

        StartCoroutine(RemoveTitleScreen());
        LevelSelect.gameObject.SetActive(true);
        SoundLibrary.Instance.PlaySound(SFX.DefaultClick);
    }

    IEnumerator RemoveTitleScreen()
    {
        yield return new WaitForSeconds(0.5f);
        TitleScreen.gameObject.SetActive(false);
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
            if (levelData.LevelID == levelID)
            {
                level = levelData;
            }
        }

        return level;
    }

    void PopulateLevelSelect()
    {
        LevelSelect.ClearMenu();

        foreach(LevelData level in LevelCreatorData.GameLevels)
        {
            bool unlocked = CheckIfLevelIsUnlocked(level.LevelID);
            LevelSelect.AddLevel(level.LevelID, unlocked);
        }

    }

    bool CheckIfLevelIsUnlocked(int levelID)
    {
        bool isUnlocked = false;
        
        // Fist level is always available
        if (levelID == 0)
        {
            isUnlocked = true;
        }
        else
        {
            if (PlayerPrefs.GetInt(LEVEL_KEY + (levelID - 1)) == 1)
            {
                isUnlocked = true;
            }
        }

        return isUnlocked;
    }

    void SaveProgress(int levelID)
    {
        PlayerPrefs.SetInt(LEVEL_KEY + levelID, 1);
    }
}
