using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("Text")]
    public TMP_Text TxtLevelName;

    [Header("Buttons")]
    public Button BtnMenu;

    [Header("Animations")]
    public Animator LevelHeader;

    public void SetLevelName(string levelName)
    {
        TxtLevelName.text = levelName;
    }

    public void LevelCompleteAnimation()
    {
        LevelHeader.Play("LevelComplete");
    }

    public void NewLevelAnimation()
    {
        LevelHeader.Play("NewLevel");
    }
}
