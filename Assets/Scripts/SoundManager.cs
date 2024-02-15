using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class SoundManager
{
    public enum Sound{
        PlayerJump,
        PlayerMove,
        PlayerDies
    }

    private static List<GameAssets.SoundAudioSource> soundAudioSources = new List<GameAssets.SoundAudioSource>();

    private static Dictionary<Sound, float> soundTimerDictionary;

    public static void Initialize() {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0f;
    }
    public static void PlaySound(Sound sound)
    {
        if (CanPlaySound(sound))
        {
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.volume = GetSoundAudioClip(sound).volume;
            audioSource.clip = GetSoundAudioClip(sound).audioClip;
            audioSource.Play();
            Object.Destroy(soundGameObject, audioSource.clip.length);
        }
    }

    public static void PlaySoundAudioSource(Sound sound)
    {
        if (HasAudioSourceForSound(sound))
        {
            GameAssets.SoundAudioSource soundAudioSource = GetSoundAudioSourceOfSound(sound);
            if (!soundAudioSource.audioSource.isPlaying) soundAudioSource.audioSource.Play(); 
        }
        else
        {
            AudioSource audioSource = GameAssets.Instance.AddComponent<AudioSource>();
            soundAudioSources.Add(new GameAssets.SoundAudioSource(sound, audioSource, GetSoundAudioClip(sound).audioClip));
        }
    }
    private static bool HasAudioSourceForSound(Sound sound)
    {
        foreach(GameAssets.SoundAudioSource soundAudioSource in soundAudioSources)
        {
            if (sound == soundAudioSource.sound)
                return true;
        }
        return false;   
    }
    private static GameAssets.SoundAudioSource GetSoundAudioSourceOfSound(Sound sound)
    {
        foreach(GameAssets.SoundAudioSource soundAudioSource in soundAudioSources)
        {
            if (sound == soundAudioSource.sound){
                return soundAudioSource;
            }
        }
        return null;
    }
    public static void StopSoundAudioSource(Sound sound)
    {
        GetSoundAudioSourceOfSound(sound).audioSource.Stop();
    }
    public static void PlaySound(AudioClip audioClip)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(audioClip);
    }

    private static bool CanPlaySound(Sound sound){
        switch(sound)
        {
            default:
                return true;

            case Sound.PlayerMove:
                if (soundTimerDictionary.ContainsKey(sound)) {
                    float lastTimePlayer = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.1f;
                    if (lastTimePlayer + playerMoveTimerMax < Time.time) {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    } 
                    else
                    {
                        return false;
                    }
                } 
                else 
                {
                    return true;
                }
        }
    }

    private static GameAssets.SoundAudioClip GetSoundAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.Instance.soundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip;
            }
        }
        Debug.LogError($"Sound {sound} not found!");
        return null;
    }
}
