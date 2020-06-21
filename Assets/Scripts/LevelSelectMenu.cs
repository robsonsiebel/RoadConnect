using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelSelectMenu : MonoBehaviour
{
    public GameObject LevelSelectButtonPrefab;
    public Transform ButtonGrid;
    public List<Button> AllButtons;

    public Action<int> OnLevelPressed;

    public void AddLevel(int levelID, bool unlocked)
    {
        Button newLevel = GameObject.Instantiate(LevelSelectButtonPrefab, ButtonGrid).GetComponent<Button>();
        
        newLevel.name = "Level " + levelID + 1;
        newLevel.GetComponentInChildren<TMP_Text>().text = (levelID + 1).ToString();
        newLevel.onClick.AddListener(() => OnLevelPressed(levelID));
        AllButtons.Add(newLevel);
        if (!unlocked)
        {
            newLevel.interactable = false;
        }    
    }

    public void ClearMenu()
    {
        foreach (Button button in AllButtons)
        {
            Destroy(button.gameObject);
        }
    }
}
