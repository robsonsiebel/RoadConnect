using System.Collections;
using UnityEngine;

public enum MusicOperation { PlaySmooth, Stop, Pause, Resume, FadeOut, Play }

public class MusicSmoothPlay : MonoBehaviour
{

    readonly float INCREMENT_DELAY = 0.5f;

    public float TargetVolume;
    public float InitialVolume;
    public float IncreaseStep;

    public AudioSource Music;

    #region Private Methods
    private void FadeOut()
    {
        if (Music.isPlaying)
            StartCoroutine(DecreaseVolume());
    }

    IEnumerator DecreaseVolume()
    {
        while (Music.volume > 0)
        {
            Music.volume -= IncreaseStep;
            yield return new WaitForSeconds(INCREMENT_DELAY);
        }

        Music.Stop();
    }

    IEnumerator IncreaseVolume(float targetVolume)
    {
        while (Music.volume < targetVolume)
        {
            Music.volume += IncreaseStep;
            yield return new WaitForSeconds(INCREMENT_DELAY);
        }
    }

    private void ResumePlayback()
    {
        Music.UnPause();
    }

    private void PausePlayback()
    {
        Music.Pause();
    }

    private void StopPlayback()
    {
        StopAllCoroutines();
        Music.Stop();
    }

    private void PlayMusicFromInitialVolume()
    {
        if (!Music.isPlaying)
        {
            Music.volume = InitialVolume;
            Music.Play();
            StartCoroutine(IncreaseVolume(TargetVolume));
        }
    }
    #endregion

    #region Public Methods
    public void SetNewAudio(AudioClip newAudio)
    {
        Music.clip = newAudio;
    }

    public void SetOperation(MusicOperation operation)
    {
        switch (operation)
        {
            case MusicOperation.PlaySmooth:
                PlayMusicFromInitialVolume();
                break;
            case MusicOperation.Stop:
                StopPlayback();
                break;
            case MusicOperation.FadeOut:
                FadeOut();
                break;
            case MusicOperation.Pause:
                PausePlayback();
                break;
            case MusicOperation.Resume:
                ResumePlayback();
                break;

        }
    }
    #endregion
}
