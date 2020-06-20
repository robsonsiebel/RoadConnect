using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LevelSelectMenu : MonoBehaviour
{

    private int m_LevelCount = 0;

    public GameObject LevelSelectButtonPrefab;
    public Transform ButtonGrid;

    public Action<string, string> OnLevelPressed;

    public void AddLevel(string levelID)
    {
        Button newLevel = GameObject.Instantiate(LevelSelectButtonPrefab, ButtonGrid).GetComponent<Button>();
        
        m_LevelCount++;
        newLevel.name = "Level " + m_LevelCount;
        newLevel.GetComponentInChildren<TMP_Text>().text = m_LevelCount.ToString();
        newLevel.onClick.AddListener(() => OnLevelPressed(levelID, newLevel.name));
        
    }
}
