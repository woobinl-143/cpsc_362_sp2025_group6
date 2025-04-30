using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Create singleton (making soundManager exclusive)
    public static SoundManager Instance { get; private set; }
    // For background music and sound effects
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioClip backgroundMusic;
    public AudioClip playerDeathSound;
    // Volume settings
    [Range(0f, 1f)] public float musicVolume = 0.1f;
    [Range(0f, 1f)] public float sfxVolume = 1.0f;
    private bool musicMuted = false;
    private bool sfxMuted = false;

    private void Awake()
    {
        // Assign soundManager
        if (Instance == null)
        {
            Instance = this;
            // Makes sure during new scenes sounds stay
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Prevent duplicate soundManagers
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Applies settings to game
        ApplyVolumeSettings();
        // Starts background music
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        // Makes sure no music is playing/muted
        if (backgroundMusic != null && !musicMuted)
        {
            musicSource.clip = backgroundMusic;
            // Loops music track
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void PlayPlayerDeathSound()
    {
        if (playerDeathSound != null && !sfxMuted)
        {
            // Plays sound over music once
            sfxSource.PlayOneShot(playerDeathSound, sfxVolume);
        }
    }

    public void SetMusicVolume(float volume)
    {
        // Adjusting music in settings
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume;
    }

    public void SetSFXVolume(float volume)
    {
        // Adjusting SFX in settings
        sfxVolume = Mathf.Clamp01(volume);
    }

    public void ToggleMusic()
    {
        // If music is toggled in settings
        musicMuted = !musicMuted;
        musicSource.mute = musicMuted;
    }

    public void ToggleSFX()
    {
        // If sfx is toggled in settings
        sfxMuted = !sfxMuted;
    }

    private void ApplyVolumeSettings()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }
}
