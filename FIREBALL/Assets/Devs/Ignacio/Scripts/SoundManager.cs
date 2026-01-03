using UnityEngine;
using UnityEngine.Audio;

public enum AudioFx
{

}

public enum AudioMusic
{

}

public enum AudioAmbience
{

}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioClip[] fxClips;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private AudioClip[] ambienceClips;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource ambienceAudioSource;
    [SerializeField] private AudioSource fxAudioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayAudioClip(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.PlayOneShot(audioClip);
    }

    public void PlayFx(AudioFx audioFx)
    {
        fxAudioSource.PlayOneShot(fxClips[(int)audioFx]);
    }

    public void PlayMusic(AudioMusic audioMusic, bool isLooping = true)
    {
        musicAudioSource.loop = isLooping;
        musicAudioSource.clip = musicClips[(int)audioMusic];
        musicAudioSource.Play();
    }

    public void PlayAmbience(AudioAmbience audioAmbience, bool isLooping = true)
    {
        ambienceAudioSource.loop = isLooping;
        ambienceAudioSource.clip = ambienceClips[(int)audioAmbience];
        ambienceAudioSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        musicAudioSource.volume = volume;  
    }

    public void SetAmbienceVolume(float volume)
    {
        ambienceAudioSource.volume = volume;  
    }

    public void SetFxVolume(float volume)
    {
        fxAudioSource.volume = volume;  
    }

    public float GetMusicVolume()
    {
        return musicAudioSource.volume;  
    }

    public float GetAmbienceVolume()
    {
        return ambienceAudioSource.volume;  
    }

    public float GetFxVolume()
    {
        return fxAudioSource.volume; 
    }
}
