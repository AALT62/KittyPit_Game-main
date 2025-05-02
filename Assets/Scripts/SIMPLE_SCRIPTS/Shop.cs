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
    private int prestigeCost = 110;
    private int prestigeLevel2Cost = 600; // Example cost for reaching prestige level 2
    private int parrotCost = 500;

    public PrestigeAnimatorController prestigeAnimatorController; // Reference to the AnimatorController script

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory == null)
            {
                Debug.LogError("PlayerInventory not found! Assign it in the Inspector or make sure it's in the scene.");
            }
        }

        if (playerMove == null)
        {
            playerMove = FindObjectOfType<PlayerMovementAdvanced>();
        }

        if (prestigeAnimatorController == null)
        {
            prestigeAnimatorController = FindObjectOfType<PrestigeAnimatorController>();
            if (prestigeAnimatorController == null)
            {
                Debug.LogError("PrestigeAnimatorController not found! Assign it in the Inspector.");
            }
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

            audioSource.PlayOneShot(shovelSound);
            UpdateUI();
        }
    }

    public void BuyPrestige()
    {
        // Check if the player can buy prestige level 1
        if (playerInventory.prestigeLevel == 0 && playerInventory.cash >= prestigeCost)
        {
            playerInventory.cash -= prestigeCost;
            playerInventory.prestigeLevel = 1;

            playerMove.PlayerRespawn();
            playerInventory.SwitchEnvironment();

            Debug.Log("Prestige level increased to 1!");

            audioSource.PlayOneShot(prestigeSound);

            // Notify the PrestigeAnimatorController to trigger the animation
            prestigeAnimatorController.TriggerPrestigeAnimation();

            // Start coroutine to reset animation flag after 3 seconds
            StartCoroutine(ResetAnimationFlag());

            UpdateUI();
        }
        // Check if the player can buy prestige level 2
        else if (playerInventory.prestigeLevel == 1 && playerInventory.cash >= prestigeLevel2Cost)
        {
            playerInventory.cash -= prestigeLevel2Cost;
            playerInventory.prestigeLevel = 2;

            playerMove.PlayerRespawn();
            playerInventory.SwitchEnvironment();

            Debug.Log("Prestige level increased to 2!");

            audioSource.PlayOneShot(prestigeSound);

            // Notify the PrestigeAnimatorController to trigger the animation
            prestigeAnimatorController.TriggerPrestigeAnimation();

            // Start coroutine to reset animation flag after 3 seconds
            StartCoroutine(ResetAnimationFlag());

            UpdateUI();
        }
    }


    public void BuyParrot()
    {
        if (!playerInventory.hasParrot && playerInventory.prestigeLevel >= 2 && playerInventory.cash >= parrotCost)
        {
            playerInventory.cash -= parrotCost;
            playerInventory.hasParrot = true;

            if (playerInventory.parrot != null)
            {
                playerInventory.parrot.SetActive(true);
            }

            Debug.Log("Parrot purchased!");

            audioSource.PlayOneShot(parrotSound);
            UpdateUI();
        }
    }

    public void RefreshUIFromOutside()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
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

        cashText.text = "Cash: $" + playerInventory.cash;
    }

    // Coroutine to reset the animation flag
    public System.Collections.IEnumerator ResetAnimationFlag()
    {
        // Wait for the animation to finish (3 seconds in this case)
        yield return new WaitForSeconds(3f);

        // Notify the AnimatorController to reset the animation
        prestigeAnimatorController.ResetAnimationFlag();
    }
}
