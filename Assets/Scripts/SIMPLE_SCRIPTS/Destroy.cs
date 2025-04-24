using UnityEngine;
using TMPro;
using UnityEngine.UI;  // For Slider UI

public class Destroy : MonoBehaviour
{
    public int blocks = 12;
    public bool colCheck = false;
    GameObject dirt;
    public GameManager gameManager;
    public int dirtValue = 1;
    private PlayerInventory playerInventory;
    public AudioClip digSound;
    private AudioSource audioSource;

    // New variables for the hold timer and progress bar
    public float baseHoldTime = 3f; // Base time before any prestige bonuses
    private float holdTimeRequired = 3f; // This will update based on prestige
    private float currentHoldTime = 0f; // The current hold time
    public Slider holdProgressBar; // Reference to the slider that represents the hold time
    public TMP_Text holdTimeText; // Reference to the text component that shows the hold time

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>(); // Get the PlayerInventory component
        audioSource = GetComponent<AudioSource>();
    }

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
    }

    void Update()
    {
        // Prevent interaction if the player has max dirt
        if (playerInventory.dirtCount >= playerInventory.dirtMax)
        {
            // Disable interaction (just return without doing anything)
            if (colCheck)
            {
                holdProgressBar.value = 0f;  // Reset progress bar immediately
                holdTimeText.text = "";  // Clear the timer text
            }
            return; // Skip the rest of the Update logic
        }

        if (colCheck && Input.GetKey(KeyCode.E))
        {
            currentHoldTime += Time.deltaTime;
            holdProgressBar.value = currentHoldTime / holdTimeRequired;

            // Update the text timer to show the remaining hold time
            float remainingTime = Mathf.Max(0f, holdTimeRequired - currentHoldTime); // Prevent negative time
            holdTimeText.text = Mathf.Ceil(remainingTime).ToString("0") + "s"; // Display in seconds

            if (currentHoldTime >= holdTimeRequired)
            {
                DestroyBlock();
            }
        }
        else
        {
            if (colCheck && Input.GetKeyUp(KeyCode.E))
            {
                currentHoldTime = 0f;
                holdProgressBar.value = 0f;
                holdTimeText.text = "";  // Clear the timer text
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
                audioSource.PlayOneShot(digSound);
                colCheck = false;

                // Add dirt to inventory and update UI
                playerInventory.dirtCount += playerInventory.dirtValue;
                Debug.Log("Total Dirt: " + playerInventory.dirtCount);

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
}
