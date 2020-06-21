using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("Text")]
    public TMP_Text TxtLevelName;
    public TMP_Text TxtEndGame;

    [Header("Buttons")]
    public Button BtnMenu;

    [Header("Animations")]
    public Animator LevelHeader;

    private void Start()
    {
        BtnMenu.onClick.AddListener(() => TxtEndGame.gameObject.SetActive(false));
    }

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

    public void AllLevelsCompleteAnimation()
    {
        TxtLevelName.text = "";
        TxtEndGame.gameObject.SetActive(true);
    }
}
