using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;
    [SerializeField]
    AudioMixer mixer;
    [SerializeField]
    AudioSource musicSource, soundSource;
    [SerializeField]
    AudioClip backgroundMusic;
    [SerializeField]
    AudioClip battleMusic;
    [SerializeField]
    AudioClip[] sounds;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this; 
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void PlayMenuBackMusic()
    {
        musicSource.Stop();
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }
    public void PlaySound(string name)
    {
        AudioClip clip = Array.Find(sounds, s => s.name == name);
        if(clip == null)
        {
            clip = sounds[0];
        }
        soundSource.PlayOneShot(clip);
    }
    public void PlayBattleTheme()
    {
        musicSource.Stop();
        musicSource.clip = battleMusic;
        musicSource.Play();
    }
    void Start()
    {
        LoadSettings();
        if(!musicSource.isPlaying)
        {
            PlayMenuBackMusic();
        }
    }
    void LoadSettings()
    {
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            SetMasterAudio(PlayerPrefs.GetFloat("MasterVolume"));
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            SetMusicVolume(PlayerPrefs.GetFloat("MusicVolume"));
        }
        if (PlayerPrefs.HasKey("EffectsVolume"))
        {
            SetEffectsVolume(PlayerPrefs.GetFloat("EffectsVolume"));
        }
    }
    public void SetMasterAudio(float volume)
    {
        mixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }
    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void SetEffectsVolume(float volume)
    {
        mixer.SetFloat("EffectsVolume", Mathf.Log10(volume) * 20);
    }
}
