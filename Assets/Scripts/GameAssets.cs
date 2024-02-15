using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    public static GameAssets Instance;

    private void Awake() {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        SoundManager.Initialize();
    }

    public SoundAudioClip[] soundAudioClips;

    [System.Serializable]
    public class SoundAudioClip {
        public SoundManager.Sound sound;
        public AudioClip audioClip;
        public float volume;
    }

    public class SoundAudioSource {
        public SoundManager.Sound sound;
        public AudioSource audioSource;
        public AudioClip audioClip;
        public float volume;

        public SoundAudioSource()
        {
        }
        public SoundAudioSource(SoundManager.Sound sound, AudioSource audioSource, AudioClip audioClip)
        {
            this.sound = sound;
            this.audioSource = audioSource;
            this.audioClip = audioClip;
            this.audioSource.clip = audioClip;
        }
    }
    
}
