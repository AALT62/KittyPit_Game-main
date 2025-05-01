using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioClip menuMusic;
    private static bool musicPlaying = false; // Tracks if music is already playing

    private void Awake()
    {
        // Ensure the AudioManager persists across scene loads
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);  // Destroy duplicates
        }
        else
        {
            DontDestroyOnLoad(gameObject); // Make sure it persists across scenes
        }
    }

    private void Start()
    {
        // Log to confirm if the AudioManager is properly initialized
        if (musicSource == null)
            Debug.LogError("AudioSource is not assigned in the AudioManager!");
        if (menuMusic == null)
            Debug.LogError("Menu music clip is not assigned in the AudioManager!");
        else
            Debug.Log("AudioManager started, menu music clip is assigned.");
    }

    public void PlayMenuMusic()
    {
        // Only play the music if it's not already playing
        if (!musicPlaying && menuMusic != null)
        {
            Debug.Log("Playing menu music...");
            musicSource.clip = menuMusic;
            musicSource.Play();
            musicPlaying = true;
        }
        else if (menuMusic == null)
        {
            Debug.LogError("No music clip assigned to play!");
        }
        else
        {
            Debug.Log("Menu music already playing.");
        }
    }

    public void StopMenuMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Stop();
            musicPlaying = false;
            Debug.Log("Menu music stopped.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);  // Check if this line triggers.

        // Only play the music if it's the "Main Menu" scene
        if (scene.name == "Main Menu")
        {
            Debug.Log("Main Menu scene loaded, playing music.");
            PlayMenuMusic();
        }
        else if (scene.name == "Empty")
        {
            Debug.Log("Empty scene loaded, stopping music.");
            StopMenuMusic();
        }
        else if (scene.name != "Credits" && scene.name != "Help")
        {
            // Stop the music only when transitioning to scenes where music should not play
            Debug.Log("Stopping music for non-menu scenes.");
            StopMenuMusic();
        }
    }

    private void OnEnable()
    {
        Debug.Log("AudioManager enabled, registering scene loaded event.");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        Debug.Log("AudioManager disabled, unregistering scene loaded event.");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
