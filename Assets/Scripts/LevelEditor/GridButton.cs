using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridButton : MonoBehaviour
{
    private Button m_Button;

    public int currentPieceId = -1;  // -1 is empty

    public Text t_button;

    public Action<int, GridButton> OnClick; 

    void Awake()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        currentPieceId++;
        OnClick.Invoke(currentPieceId, this);
    }

    public void UpdateSprite(Sprite newSprite)
    {
        m_Button.image.sprite = newSprite;
        t_button.text = "";
    }

    public void SetEmpty()
    {
        currentPieceId = -1;
        m_Button.image.sprite = null;
        t_button.text = "Add Piece";
    }
}
