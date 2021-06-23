using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource SfxSource;
    public AudioSource musicSource;
    public static AudioManager instance = null;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    public void PlayMusic()
    {
        musicSource.Play();
    }


    public void PlayMusic(AudioClip music)
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }


    public void PauseMusic()
    { 
        musicSource.Pause();
    }

    public void UnpauseMusic()
    {
        musicSource.UnPause();
    }

    public void ButtonClickSFX(AudioClip _SFX)
    {
        transform.GetComponent<AudioSource>().PlayOneShot(_SFX);
    }

    public void PlaySingle(AudioClip clip)
    {
        SfxSource.PlayOneShot(clip);
    }
    public void PlaySingle(AudioClip clip,float _volume)
    {
        SfxSource.PlayOneShot(clip, _volume);
    }

}
