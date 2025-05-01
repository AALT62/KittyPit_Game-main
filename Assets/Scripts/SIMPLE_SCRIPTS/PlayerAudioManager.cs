using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    private AudioSource sfxSource;
    private AudioSource musicSource;

    void Awake()
    {
        AudioSource[] sources = GetComponents<AudioSource>();

        if (sources.Length >= 2)
        {
            sfxSource = sources[0];   // Assign based on order
            musicSource = sources[1]; // You can swap if needed
        }
        else
        {
            Debug.LogError("Not enough AudioSources on Player GameObject!");
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
}
