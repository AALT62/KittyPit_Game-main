using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Destroy : MonoBehaviour
{
    public int blocks = 12;
    public bool colCheck = false;
    GameObject dirt;
    public GameManager gameManager;
    public int dirtValue = 1;
    private PlayerInventory playerInventory;
    public AudioClip digSound;
    public AudioClip backgroundMusic;
    private AudioSource audioSource; // For background music

    // New variables for the hold timer and progress bar
    public float baseHoldTime = 3f; // Base time before any prestige bonuses
    private float holdTimeRequired = 3f; // This will update based on prestige
    private float currentHoldTime = 0f; // The current hold time
    public Slider holdProgressBar; // Reference to the slider that represents the hold time
    public TMP_Text holdTimeText; // Reference to the text component that shows the hold time

    private bool isDigging = false; // Track if the digging sound is playing

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>(); // Get the PlayerInventory component
        audioSource = GetComponent<AudioSource>();  // Get the AudioSource component for background music
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.volume = 0.005f; // Set background music volume (adjust as needed)
        audioSource.Play(); // Start playing background music immediately
        // Create and assign a new AudioSource for sound effects
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true; // Ensure the sound effect loops while held
    }


//CONFLICT - FIX THIS
    /*
    public AudioSource audioSource;
    //public Animation Dig;

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        audioSource = GetComponent<AudioSource>();
        //Dig = gameObject.GetComponent<Animation>();
    }
    */

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Diggable"))
        {
            colCheck = true;
            dirt = other.gameObject;

            // Update hold time required based on prestige level
            if (playerInventory != null)
            {
                if (playerInventory.hasUpgradedShovel)
                {
                    holdTimeRequired = 2f; // Shorter time if the shovel is upgraded
                }
                else
                {
                    holdTimeRequired = baseHoldTime; // Default time
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colCheck = false;
        currentHoldTime = 0f;  // Reset hold time when exiting the collider
        holdProgressBar.value = 0f;  // Reset progress bar
        holdTimeText.text = "";  // Clear the timer text

        // Stop the digging sound when player exits the trigger zone
        StopDiggingSound();
    }

    void Update()
    {
        // Prevent interaction if the player has max dirt
        if (playerInventory.dirtCount >= playerInventory.dirtMax)
        {
            if (colCheck)
            {
                holdProgressBar.value = 0f;
                holdTimeText.text = "";
            }
            return;
        }

        if (colCheck)
        {
            if (Input.GetKey(KeyCode.E))
            {
                // Start the digging sound if not already playing
                if (!isDigging)
                {
                    StartDiggingSound();
                }

                currentHoldTime += Time.deltaTime;
                holdProgressBar.value = currentHoldTime / holdTimeRequired;

                // Update the text timer to show the remaining hold time
                float remainingTime = Mathf.Max(0f, holdTimeRequired - currentHoldTime);
                holdTimeText.text = Mathf.Ceil(remainingTime).ToString("0") + "s";

                if (currentHoldTime >= holdTimeRequired)
                {
                    DestroyBlock();  // Only destroy after enough time held
                }
            }
            else
            {
                // If E key released, reset everything
                if (isDigging)
                {
                    StopDiggingSound();
                }
                currentHoldTime = 0f;
                holdProgressBar.value = 0f;
                holdTimeText.text = "";
            }
        }
    }


    private void DestroyBlock()
    {
        if (dirt != null)
        {
            if (playerInventory.dirtCount < playerInventory.dirtMax)
            {
                Destroy(dirt);
                colCheck = false;

                // Add dirt to inventory and update UI
                playerInventory.dirtCount += playerInventory.dirtValue;

                playerInventory.UpdateInventoryUI();
                Debug.Log("Total Dirt: " + playerInventory.dirtCount);
                StopDiggingSound();
                blocks -= 1;

                // Check if the player has won or if there's any other game logic
                if (gameManager != null)
                {
                    gameManager.WinCheck();
                }

                // Reset the progress bar and hold timer
                holdProgressBar.value = 0f;
                currentHoldTime = 0f;
                holdTimeText.text = "";  // Clear the timer text
            }
        }
    }

    // Method to start the digging sound and make it loop
    private void StartDiggingSound()
    {
        audioSource.clip = digSound;  // Set the clip for sound effect
        audioSource.loop = true;  // Set the sound to loop while holding E
        audioSource.Play();  // Start the sound (it will loop automatically)
        isDigging = true;  // Mark that the player is actively digging
    }

    // Method to stop the digging sound when released
    private void StopDiggingSound()
    {
        audioSource.loop = false;  // Stop looping the sound
        audioSource.Stop();  // Stop the sound entirely
        isDigging = false;  // Mark that the player is no longer digging
    }
}
