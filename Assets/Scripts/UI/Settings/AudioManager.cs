using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public enum AudioType
    {
        Walk,
        Attack,
        Death,
        Chest,
        Equipment,
        SavePoint,
        PowerUp,
        Potion,
        SlideRock,
        MenuMusic,
        GameMusic,
        MenuAmbiant,
        GameAmbiant,
    }

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer _mixer;

    [System.Serializable]
    public struct AudioData
    {
        public AudioType type;
        public AudioSource source;
    }

    public AudioData[] audioDatas;

    private void Awake()
    {
        if (Instance == null )
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void Start()
    {
        VolumnSettings.LoadVolumnInGame(_mixer);
    }

    public void PlaySound(AudioType type)
    {
        AudioData data = GetAudioData(type);
        if (data.source.isPlaying) { return; }
        data.source.Play();
    }

    public void StopSound(AudioType type)
    {
        AudioData data = GetAudioData(type);
        try
        {
            data.source.Stop();
        } catch (Exception e) {  Debug.LogException(e); }
    }

    public void StopAllSound()
    {
        foreach (AudioData audioData in audioDatas)
        {
            audioData.source.Stop();
        }
    }

    public AudioData GetAudioData(AudioType type)
    {
        for (int i = 0; i < audioDatas.Length; i++)
        {
            if (audioDatas[i].type == type)
            {
                return audioDatas[i];
            }
        }
        Debug.LogError("AudioManager: No clip found for type " + type);
        return new AudioData();
    }
}
