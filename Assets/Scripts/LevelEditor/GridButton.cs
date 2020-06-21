using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridButton : MonoBehaviour
{
    private string DEFAULT_TEXT = "Add Piece";

    private Button m_Button;

    public int CurrentPieceId = -1;  // -1 is empty
    public int StartRotation = 0;
    public int TargetRotation = 0;

    public Text t_button;

    public Action<GridButton> OnClick; 

    void Awake()
    {
        m_Button = GetComponent<Button>();
        m_Button.onClick.AddListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        OnClick.Invoke(this);
    }

    public void UpdateSprite(Sprite newSprite)
    {
        m_Button.image.sprite = newSprite;
        t_button.text = "";
    }

    public void SetEmpty()
    {
        CurrentPieceId = -1;
        m_Button.image.sprite = null;
        t_button.text = DEFAULT_TEXT;
        StartRotation = 0;
        TargetRotation = 0;
        m_Button.transform.localEulerAngles = Vector3.zero;
    }

    public void SetVisible(bool isVisible)
    {
        if (isVisible)
        {
            m_Button.interactable = true;
            m_Button.image.color = Color.white;
            t_button.text = DEFAULT_TEXT;
        }
        else
        {
            m_Button.interactable = false;
            m_Button.image.color = new Color(0, 0, 0, 0);
            t_button.text = "";
        }
        
    }

    public void SetStartRotation()
    {
        Rotate();
        StartRotation = (int)m_Button.transform.localEulerAngles.z;
    }

    public void SetTargetRotation()
    {
        Rotate();
        TargetRotation = (int)m_Button.transform.localEulerAngles.z;
    }

    public void SwitchToTargetRotation()
    {
        m_Button.transform.localEulerAngles = new Vector3(0,0,TargetRotation);
    }

    public void SwitchToStartRotation()
    {
        m_Button.transform.localEulerAngles = new Vector3(0, 0, StartRotation);
    }

    void Rotate()
    {
        m_Button.transform.localEulerAngles = new Vector3(0, 0, m_Button.transform.localEulerAngles.z - 90);
    }
}
