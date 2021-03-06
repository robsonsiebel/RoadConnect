﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum LevelEditorMode { Layout, Start, Solution}

public class LevelEditor : MonoBehaviour
{
    private string LEVEL_PREFIX = "Level";

    private int m_Rows = 4;
    private int m_Columns = 4;
    private bool m_LevelLoaded = false;
    private LevelEditorMode m_CurrentMode = LevelEditorMode.Layout;
    private List<GridButton> m_AllButtons = new List<GridButton>();

    [Header("GameObjects")]
    public GameObject GridButtonPrefab;
    public GameObject Grid;

    [Header("Data")]
    public LevelCreatorData LevelCreatorData;
    
    [Header("Buttons")]
    public Button BtnLayout;
    public Button BtnStartRotation;
    public Button BtnSolution;
    public Button BtnSave;
    public Button BtnClear;
    public Button BtnClose;

    [Header("Others")]
    public TMP_Text t_status;
    public TMP_Text t_editor_mode;
    public TMP_Dropdown LevelSelectorDropdown;

    #region Private
    private void Start()
    {
        BtnLayout.onClick.AddListener(() =>
        {
            m_CurrentMode = LevelEditorMode.Layout;
            OnEditorModeChanged();
        });
        BtnStartRotation.onClick.AddListener(() =>
        {
            m_CurrentMode = LevelEditorMode.Start;
            OnEditorModeChanged();
        });
        BtnSolution.onClick.AddListener(() =>
        {
            m_CurrentMode = LevelEditorMode.Solution;
            OnEditorModeChanged();
        });

        BtnSave.onClick.AddListener(SaveLevel);
        BtnClear.onClick.AddListener(ClearLevel);

        PopulateGrid();
        BtnLayout.interactable = false;

        PopulateDropdown();

        LevelSelectorDropdown.onValueChanged.AddListener(delegate
        {
            OnValueSelected(LevelSelectorDropdown);
        });

        BtnClose.onClick.AddListener(() => Destroy(gameObject));
    }

    private void ClearLevel()
    {
        foreach(GridButton button in m_AllButtons)
        {
            button.SetEmpty();
        }

        t_status.text = "Clean slate!";
        m_LevelLoaded = false;
    }

    private void OnValueSelected(TMP_Dropdown levelSelectorDropdown)
    {
        foreach(LevelData level in LevelCreatorData.GameLevels)
        {
            if (level.LevelID == levelSelectorDropdown.value)
            {
                LoadLevel(level);
            }
        }
    }

    private void LoadLevel(LevelData level)
    {
        m_LevelLoaded = true;
        t_status.text = "Loaded level " + level.LevelID;

        for (int i = 0; i < m_AllButtons.Count; i++)
        {
            m_AllButtons[i].SetEmpty();
            m_AllButtons[i].CurrentPieceId = level.AllPieces[i].PieceID;
            m_AllButtons[i].StartRotation = level.AllPieces[i].StartRotation;
            m_AllButtons[i].TargetRotation = level.AllPieces[i].TargetRotation;

            if (m_AllButtons[i].CurrentPieceId != -1)
            {
                m_AllButtons[i].UpdateSprite(LevelCreatorData.LevelSprites[m_AllButtons[i].CurrentPieceId]);
            }
        }
    }

    private void PopulateDropdown()
    {
        List<string> options = new List<string>();
        foreach (LevelData levelData in LevelCreatorData.GameLevels)
        {
            options.Add(levelData.LevelID.ToString());
        }
        LevelSelectorDropdown.ClearOptions();
        LevelSelectorDropdown.AddOptions(options);
    }

    private void PopulateGrid()
    {
        GridButton newButton;

        for(int r = 0; r < m_Rows; r++)
        {
            for (int c = 0; c < m_Columns; c++)
            {
                newButton = GameObject.Instantiate(GridButtonPrefab, Grid.gameObject.transform).GetComponent<GridButton>();
                m_AllButtons.Add(newButton);
                newButton.name = "Piece" + r + c;
                newButton.OnClick += OnGridButtonClicked;
            }
        }
    }

    private void OnGridButtonClicked(GridButton button)
    {
        if (m_CurrentMode == LevelEditorMode.Layout)
        {
            button.CurrentPieceId++;
            if (button.CurrentPieceId >= LevelCreatorData.LevelSprites.Length)
            {
                button.SetEmpty();

            }
            else
            {
                button.UpdateSprite(LevelCreatorData.LevelSprites[button.CurrentPieceId]);
            }
            
        }else
        if (m_CurrentMode == LevelEditorMode.Start)
        {
            button.SetStartRotation();
        }else
        if (m_CurrentMode == LevelEditorMode.Solution)
        {
            button.SetTargetRotation();
        }

        BtnSave.interactable = CheckSaveButtonEnable();
    }

    // So that an empty level cannot be saved
    private bool CheckSaveButtonEnable()
    {
        bool gridHasAtLeastOnePiece = false;

        foreach (GridButton piece in m_AllButtons)
        {
            if (piece.CurrentPieceId != -1)
            {
                gridHasAtLeastOnePiece = true;
            }
        }

        return gridHasAtLeastOnePiece;
    }

    private void OnEditorModeChanged()
    {
        switch (m_CurrentMode)
        {
            case LevelEditorMode.Layout:
                BtnLayout.interactable = false;
                BtnStartRotation.interactable = true;
                BtnSolution.interactable = true;
                t_editor_mode.text = "Layout";
                SetUnusedTilesVisible(true);
                LevelSelectorDropdown.interactable = true;
                break;
            case LevelEditorMode.Start:
                BtnLayout.interactable = true;
                BtnStartRotation.interactable = false;
                BtnSolution.interactable = true;
                t_editor_mode.text = "Starting Rotation";
                SetUnusedTilesVisible(false);
                SwitchToStartingRotation();
                LevelSelectorDropdown.interactable = false;
                break;
            case LevelEditorMode.Solution:
                BtnLayout.interactable = true;
                BtnSolution.interactable = false;
                BtnStartRotation.interactable = true;
                t_editor_mode.text = "Target Rotation";
                SetUnusedTilesVisible(false);
                SwitchToTargetRotation();
                LevelSelectorDropdown.interactable = false;
                break;
        }
    }

    private void SetUnusedTilesVisible(bool visible)
    {
        foreach(GridButton piece in m_AllButtons)
        {
            if (piece.CurrentPieceId == -1)
            {
                piece.SetVisible(visible);
            }
        }
    }

    private void SwitchToTargetRotation()
    {
        foreach (GridButton piece in m_AllButtons)
        {
            if (piece.CurrentPieceId != -1)
            {
                piece.SwitchToTargetRotation();
            }
        }
    }

    private void SwitchToStartingRotation()
    {
        foreach (GridButton piece in m_AllButtons)
        {
            if (piece.CurrentPieceId != -1)
            {
                piece.SwitchToStartRotation();
            }
        }
    }

    private void SaveLevel()
    {
        LevelData newLevel = new LevelData();
        foreach (GridButton button in m_AllButtons)
        {
            newLevel.AllPieces.Add(new PieceData(button.CurrentPieceId, button.StartRotation, button.TargetRotation));
        }

        if (m_LevelLoaded)
        {
            t_status.text = "Level " + LevelSelectorDropdown.value + " replaced!";
            LevelCreatorData.ReplaceLevel(LevelSelectorDropdown.value, newLevel);
        }
        else
        {
            t_status.text = "New Level saved!";
            LevelCreatorData.AddNewNevel(newLevel);
        }

        PopulateDropdown();
    }
    #endregion

}
