using UnityEngine;
using TMPro;
using UnityEngine.UI;  // For Slider UI

public class Shop : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TMP_Text shovelPriceText;
    public TMP_Text prestigePriceText;
    public TMP_Text parrotPriceText;
    public TMP_Text cashText; // Cash Text in Shop Panel

    public Button shovelButton;
    public Button prestigeButton;
    public Button parrotButton;

    public AudioClip shovelSound;  // Sound effect for buying shovel
    public AudioClip prestigeSound;  // Sound effect for buying prestige
    public AudioClip parrotSound; // Sound effect for buying parrot
    private AudioSource audioSource;
    public PlayerMovementAdvanced playerMove;
    private int shovelUpgradeCost = 150;
    private int prestigeCost = 300;
    private int prestigeLevel2Cost = 600; // Example cost for reaching prestige level 2
    private int parrotCost = 500;
    public Animator animator;


    private void Start()
    {
        // Initialize the audioSource reference
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        // Auto-assign playerInventory if it's not set in Inspector
        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory == null)
            {
                Debug.LogError("PlayerInventory not found! Assign it in the Inspector or make sure it's in the scene.");
            }
        }

        // Auto-assign playerMove if not already set
        if (playerMove == null)
        {
            playerMove = FindObjectOfType<PlayerMovementAdvanced>();
        }

        UpdateUI();
    }
    public void BuyShovelUpgrade()
    {
        if (!playerInventory.hasUpgradedShovel && playerInventory.cash >= shovelUpgradeCost)
        {
            playerInventory.cash -= shovelUpgradeCost;
            playerInventory.hasUpgradedShovel = true;
            playerInventory.SwitchShovel();
            playerInventory.holdTime = 2f; // Apply effect for shovel upgrade
            Debug.Log("Shovel upgraded!");

            // Play shovel purchase sound
            audioSource.PlayOneShot(shovelSound);

            // Update the UI
            UpdateUI();
        }
    }

    public void BuyPrestige()
    {
        if (playerInventory.prestigeLevel == 0 && playerInventory.cash >= prestigeCost)
        {
            playerInventory.cash -= prestigeCost;
            playerInventory.prestigeLevel = 1;

            // Switch to new environment for level 1
            playerMove.PlayerRespawn();
            playerInventory.SwitchEnvironment();

            Debug.Log("Prestige level increased to 1!");

            // Play prestige purchase sound
            audioSource.PlayOneShot(prestigeSound);

            

            // Update the UI
            UpdateUI();
        }
        else if (playerInventory.prestigeLevel == 1 && playerInventory.cash >= prestigeLevel2Cost)
        {
            playerInventory.cash -= prestigeLevel2Cost;
            playerInventory.prestigeLevel = 2;
            playerMove.PlayerRespawn();
            playerInventory.SwitchEnvironment();
            // Switch to new environment for level 2 (optional)
            // playerInventory.SwitchToPrestigeLevel2(); // Add this if you want a separate environment

            Debug.Log("Prestige level increased to 2!");

            // Play prestige purchase sound
            audioSource.PlayOneShot(prestigeSound);

            // Update the UI
            UpdateUI();
        }
    }


    public void BuyParrot()
    {
        if (!playerInventory.hasParrot && playerInventory.prestigeLevel >= 2 && playerInventory.cash >= parrotCost)
        {
            playerInventory.cash -= parrotCost;
            playerInventory.hasParrot = true;

            // Make the parrot visible once purchased
            if (playerInventory.parrot != null)
            {
                playerInventory.parrot.SetActive(true); // Make the parrot visible
            }

            Debug.Log("Parrot purchased!");

            // Play parrot purchase sound
            audioSource.PlayOneShot(parrotSound);

            // Update the UI
            UpdateUI();
        }
    }


    public void RefreshUIFromOutside()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        // Update shovel text
        if (playerInventory.hasUpgradedShovel)
        {
            shovelPriceText.text = "Purchased";
            shovelButton.interactable = false;
        }
        else
        {
            shovelPriceText.text = "$" + shovelUpgradeCost;
            shovelButton.interactable = playerInventory.cash >= shovelUpgradeCost;
        }

        // Update prestige text and button
        if (playerInventory.prestigeLevel == 0)
        {
            prestigePriceText.text = "$" + prestigeCost;
            prestigeButton.interactable = playerInventory.cash >= prestigeCost;
        }
        else if (playerInventory.prestigeLevel == 1)
        {
            prestigePriceText.text = "$" + prestigeLevel2Cost;
            prestigeButton.interactable = playerInventory.cash >= prestigeLevel2Cost;
        }
        else
        {
            prestigePriceText.text = "Prestiged";
            prestigeButton.interactable = false;
        }

        // Update parrot text and button
        if (playerInventory.hasParrot)
        {
            parrotPriceText.text = "Owned";
            parrotButton.interactable = false;
        }
        else
        {
            parrotPriceText.text = "$" + parrotCost;
            parrotButton.interactable = playerInventory.prestigeLevel >= 2 && playerInventory.cash >= parrotCost;
        }

        // Update cash display
        cashText.text = "Cash: $" + playerInventory.cash;
    }


}
