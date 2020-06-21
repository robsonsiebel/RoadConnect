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
    public Button BtnPlay;

    [Header("Animations")]
    public Animator LevelHeader;

    #region Private
    private void Start()
    {
        BtnPlay.gameObject.SetActive(false);
        BtnMenu.onClick.AddListener(() => TxtEndGame.gameObject.SetActive(false));
    }
    #endregion

    #region Public
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

    public void ButtonPlayAppear()
    {
        BtnPlay.gameObject.transform.localScale = Vector3.zero;
        BtnPlay.gameObject.SetActive(true);        
        LeanTween.scale(BtnPlay.gameObject, Vector3.one, 0.5f).setEaseInOutQuad();
    }
    #endregion
}
