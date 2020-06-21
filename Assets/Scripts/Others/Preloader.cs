using UnityEngine;

public class Preloader : MonoBehaviour
{
    private readonly string GAME_KEY = "FIRST_BOOT";

    #region Private Methods
    private void Awake()
    {
        if (PlayerPrefs.GetInt(GAME_KEY) == 0)
        {
            PlayerPrefs.SetInt(GAME_KEY, 1);
            GamePreferences.SaveInitialPreferences();
        }
        else
        {
            GamePreferences.LoadPreferences();
        }
    }

    private void Start()
    {
        SceneController.RequestLoadScene(GameScene.Main);
    }
    #endregion
}
