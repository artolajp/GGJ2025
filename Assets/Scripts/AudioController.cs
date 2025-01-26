using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    //Declare variables:
    public static AudioController instance;
    public Audio[] sounds;

    //Events:
    void Awake()
    {
        //Make persistant.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);

            return;
        }

        DontDestroyOnLoad(gameObject);

        //Asing audio sources.
        foreach (Audio sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.outputAudioMixerGroup = sound.outputAudioMixerGroup;
            sound.source.loop = sound.loop;
            sound.source.priority = sound.priority;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.spatialBlend = sound.spatialBlend;
            sound.source.minDistance = sound.minimumDistance;
            sound.source.maxDistance = sound.maximumDistance;
        }
    }

    //Functions:
    public void AudioPlaySound(params string[] names)
    {
        int randomSoundIndex = UnityEngine.Random.Range(0, names.Length);
        Audio currentSound = Array.Find(sounds, sound => sound.clip.name == names[randomSoundIndex]);

        if (currentSound == null)
        {
            Debug.Log("Audio: " + names[randomSoundIndex] + " not found!");

            return;
        }

        currentSound.source.Play();
    }

    public void AudioPlaySoundVariation(float minimumPitch = 1f, float maximumPitch = 3, params string[] names)
    {
        int randomSoundIndex = UnityEngine.Random.Range(0, names.Length);
        Audio currentSound = Array.Find(sounds, sound => sound.clip.name == names[randomSoundIndex]);

        if (currentSound == null)
        {
            Debug.Log("Audio: " + names[randomSoundIndex] + " not found!");

            return;
        }

        currentSound.source.pitch = Mathf.Clamp(UnityEngine.Random.Range(minimumPitch, maximumPitch), 0.1f, 3);
        currentSound.source.Play();
    }

    public void AudioPlaySoundVariation(float minimumPitch = 1f, float maximumPitch = 3, float setSpatialBlend = 1, params string[] names)
    {
        int randomSoundIndex = UnityEngine.Random.Range(0, names.Length);
        Audio currentSound = Array.Find(sounds, sound => sound.clip.name == names[randomSoundIndex]);

        if (currentSound == null)
        {
            Debug.Log("Audio: " + names[randomSoundIndex] + " not found!");

            return;
        }

        currentSound.source.pitch = Mathf.Clamp(UnityEngine.Random.Range(minimumPitch, maximumPitch), 0.1f, 3);
        currentSound.source.spatialBlend = setSpatialBlend;
        currentSound.source.Play();
    }

    public void AudioPlaySoundVariationAtPosition(Vector3 position, float minimumPitch = 1f, float maximumPitch = 3, params string[] names)
    {
        int randomSoundIndex = UnityEngine.Random.Range(0, names.Length);
        Audio currentSound = Array.Find(sounds, sound => sound.clip.name == names[randomSoundIndex]);

        if (currentSound == null)
        {
            Debug.Log("Audio: " + names[randomSoundIndex] + " not found!");

            return;
        }

        currentSound.source.pitch = Mathf.Clamp(UnityEngine.Random.Range(minimumPitch, maximumPitch), 0.1f, 3);
        AudioSource.PlayClipAtPoint(currentSound.source.clip, position);
    }

    public void AudioPlaySoundVariationAtPosition(Vector3 position, float minimumPitch = 1f, float maximumPitch = 3, int setSpatialBlend = 1, params string[] names)
    {
        int randomSoundIndex = UnityEngine.Random.Range(0, names.Length);
        Audio currentSound = Array.Find(sounds, sound => sound.clip.name == names[randomSoundIndex]);

        if (currentSound == null)
        {
            Debug.Log("Audio: " + names[randomSoundIndex] + " not found!");

            return;
        }

        currentSound.source.pitch = Mathf.Clamp(UnityEngine.Random.Range(minimumPitch, maximumPitch), 0.1f, 3);
        currentSound.source.spatialBlend = setSpatialBlend;
        AudioSource.PlayClipAtPoint(currentSound.source.clip, position);
    }
}

[System.Serializable]
public class Audio
{
    //Declare variables:
    [System.NonSerialized] public AudioSource source;
    public AudioClip clip;
    public AudioMixerGroup outputAudioMixerGroup;
    public bool loop = false;
    [Range(0, 256)] public int priority = 128;
    [Range(0f, 1f)] public float volume = 1;
    [Range(0.1f, 3f)] public float pitch = 1;
    [Range(-1f, 1f)] public float stereoPan = 0;
    [Range(0f, 1f)] public float spatialBlend = 1;
    public float minimumDistance = 0.5f;
    public float maximumDistance = 1.5f;
}