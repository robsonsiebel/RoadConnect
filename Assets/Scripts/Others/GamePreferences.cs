using UnityEngine;

public static class GamePreferences
{
    readonly static string SoundKey = "Sound";
    readonly static string MusicKey = "Music";

    private static bool soundOn = true;
    private static bool musicOn = true;

    #region Properties
    public static bool SoundOn
    {
        get
        {
            return soundOn;
        }

        set
        {
            soundOn = value;
        }
    }

    public static bool MusicOn
    {
        get
        {
            return musicOn;
        }

        set
        {
            musicOn = value;
        }
    }
    #endregion

    #region Public Methods
    public static void SavePreferences()
    {
        PlayerPrefs.SetInt(SoundKey, SoundOn ? 1 : 0);
        PlayerPrefs.SetInt(MusicKey, MusicOn ? 1 : 0);
    }

    public static void LoadPreferences()
    {
        SoundOn = PlayerPrefs.GetInt(SoundKey) == 1 ? true : false;
        MusicOn = PlayerPrefs.GetInt(MusicKey) == 1 ? true : false;
    }

    public static void SaveInitialPreferences()
    {
        PlayerPrefs.SetInt(SoundKey, 1);
        PlayerPrefs.SetInt(MusicKey, 1);
    }
    #endregion
}
