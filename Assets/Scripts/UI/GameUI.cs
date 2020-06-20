using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public TMP_Text TxtLevelName;
    public Button BtnMenu;

    public void SetLevelName(string levelName)
    {
        TxtLevelName.text = levelName;
    }

}
