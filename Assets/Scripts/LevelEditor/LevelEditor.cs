using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum LevelEditorMode { Layout, Start, Solution}

public class LevelEditor : MonoBehaviour
{
    private int m_rows = 4;
    private int m_columns = 4;

    private LevelEditorMode m_CurrentMode = LevelEditorMode.Layout;

    private List<GridButton> m_AllButtons = new List<GridButton>();

    public GameObject GridButtonPrefab;
    public GameObject Grid;

    public LevelCreatorData LevelCreatorData;

    public TMP_Text t_editor_mode;

    public Button BtnLayout;
    public Button BtnStartRotation;
    public Button BtnSolution;

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
        PopulateGrid();
        BtnLayout.interactable = false;
    }

    private void PopulateGrid()
    {
        GridButton newButton;

        for(int r = 0; r < m_rows; r++)
        {
            for (int c = 0; c < m_columns; c++)
            {
                newButton = GameObject.Instantiate(GridButtonPrefab, Grid.gameObject.transform).GetComponent<GridButton>();
                m_AllButtons.Add(newButton);
                newButton.OnClick += OnGridButtonClicked;
            }
        }
    }

    private void OnGridButtonClicked(int id, GridButton button)
    {
        if (m_CurrentMode == LevelEditorMode.Layout)
        {
            if (id >= LevelCreatorData.LevelSprites.Length)
            {
                id = -1;
                button.SetEmpty();
                return;
            }

            button.UpdateSprite(LevelCreatorData.LevelSprites[id]);
        }else
        if (m_CurrentMode == LevelEditorMode.Start)
        {
            button.SetStartRotation();
        }else
        if (m_CurrentMode == LevelEditorMode.Solution)
        {
            button.SetTargetRotation();
        }
    }

    void OnEditorModeChanged()
    {
        switch (m_CurrentMode)
        {
            case LevelEditorMode.Layout:
                BtnLayout.interactable = false;
                BtnStartRotation.interactable = true;
                BtnSolution.interactable = true;
                t_editor_mode.text = "Layout";
                SetUnusedTilesVisible(true);
                break;
            case LevelEditorMode.Start:
                BtnLayout.interactable = true;
                BtnStartRotation.interactable = false;
                BtnSolution.interactable = true;
                t_editor_mode.text = "Starting Rotation";
                SetUnusedTilesVisible(false);
                break;
            case LevelEditorMode.Solution:
                BtnLayout.interactable = true;
                BtnSolution.interactable = false;
                BtnStartRotation.interactable = true;
                t_editor_mode.text = "Target Rotation";
                SetUnusedTilesVisible(false);
                break;
        }
    }

    void SetUnusedTilesVisible(bool visible)
    {
        foreach(GridButton piece in m_AllButtons)
        {
            if (piece.CurrentPieceId == -1)
            {
                piece.SetVisible(visible);
            }
        }
    }


}
