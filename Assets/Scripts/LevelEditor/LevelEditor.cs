using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelEditor : MonoBehaviour
{
    private int m_rows = 4;
    private int m_columns = 4;

    private List<GridButton> m_AllButtons = new List<GridButton>();

    public GameObject GridButtonPrefab;
    public GameObject Grid;

    public LevelCreatorData LevelCreatorData;

    private void Start()
    {
        PopulateGrid();
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
        if (id >= LevelCreatorData.LevelSprites.Length)
        {
            id = -1;
            button.SetEmpty();
            return;
        }

        button.UpdateSprite(LevelCreatorData.LevelSprites[id]);
    }
}
