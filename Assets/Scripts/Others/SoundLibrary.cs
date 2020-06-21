using UnityEngine;

public enum SFX
{
    DefaultClick,
    ShapeRotate,
    ShapeAppear,
    LevelComplete
}



public class SoundLibrary : SingletonMonobehavior<SoundLibrary>
{
    //[SerializeField]
   // SettingsViewController Settings;

    [SerializeField]
    MusicSmoothPlay GameMusic;

    #region Audio Sources

    [SerializeField]
    AudioSource m_defaultClick;

    [SerializeField]
    AudioSource m_ShapeRotate;

    [SerializeField]
    AudioSource m_ShapeAppear;

    [SerializeField]
    AudioSource m_LevelComplete;

    #endregion

    private void Start()
    {
        //Settings.EventToggleMusic += OnToggleMusic;
    }

    #region Sound
    public void PlaySound(SFX soundId, float delay, bool loops, float volume, float pitch)
    {

        if (!GamePreferences.SoundOn) return;

        switch (soundId)
        {
            case SFX.DefaultClick:
                PlayRequestedSound(m_defaultClick, delay, loops, volume, pitch);
                break;
            case SFX.ShapeRotate:
                PlayRequestedSound(m_ShapeRotate, delay, loops, volume, pitch);
                break;
            case SFX.ShapeAppear:
                PlayRequestedSound(m_ShapeAppear, delay, loops, volume, pitch);
                break;
            case SFX.LevelComplete:
                PlayRequestedSound(m_LevelComplete, delay, loops, volume, pitch);
                break;
        }

    }

    public void PlaySound(SFX soundId)
    {
        PlaySound(soundId, 0, false, 1, 1);
    }

    public void PlaySound(SFX soundId, float volume)
    {
        PlaySound(soundId, 0, false, volume, 1);
    }

    private void PlayRequestedSound(AudioSource audio, float delay, bool loops, float volume, float pitch)
    {
        if (!audio.isPlaying)
        {
            audio.pitch = pitch;
            audio.volume = volume;
            audio.loop = loops;
            audio.PlayDelayed(delay);
        }
    }
    #endregion

    #region Music
    private void OnToggleMusic(bool musicOn)
    {
        if (musicOn)
        {
            PlayMusic();
        }
        else
        {
            StopMusic();
        }
    }

    public void PlayMusic()
    {
        if (!GamePreferences.MusicOn) return;

        GameMusic.SetOperation(MusicOperation.PlaySmooth);
    }

    public void PauseMusic()
    {
        GameMusic.SetOperation(MusicOperation.Pause);
    }

    public void ResumeMusic()
    {
        if (!GamePreferences.MusicOn) return;

        GameMusic.SetOperation(MusicOperation.Resume);
    }

    public void StopMusic()
    {
        GameMusic.SetOperation(MusicOperation.Stop);
    }

    public void FadeOutMusic()
    {
        GameMusic.SetOperation(MusicOperation.FadeOut);
    }
    #endregion

}
