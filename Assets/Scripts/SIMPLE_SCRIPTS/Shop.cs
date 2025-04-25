using UnityEngine;
using TMPro;
using UnityEngine.UI;  // For Slider UI

public class Shop : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TMP_Text shovelPriceText;
    public TMP_Text prestigePriceText;
    public TMP_Text cashText; // Cash Text in Shop Panel

    public Button shovelButton;
    public Button prestigeButton;

    public AudioClip shovelSound;  // Sound effect for buying shovel
    public AudioClip prestigeSound;  // Sound effect for buying prestige
    private AudioSource audioSource;

    private int shovelUpgradeCost = 150;
    private int prestigeCost = 300;

    private void Start()
    {
        // Initialize the audioSource reference
        audioSource = GetComponent<AudioSource>();

        // Update the UI
        UpdateUI();
    }

    public void BuyShovelUpgrade()
    {
        if (!playerInventory.hasUpgradedShovel && playerInventory.cash >= shovelUpgradeCost)
        {
            playerInventory.cash -= shovelUpgradeCost;
            playerInventory.hasUpgradedShovel = true;
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

            // Switch to new environment
            playerInventory.SwitchEnvironment();

            Debug.Log("Prestige level increased!");

            // Play prestige purchase sound
            audioSource.PlayOneShot(prestigeSound);

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
        // Text labels
        shovelPriceText.text = playerInventory.hasUpgradedShovel ? "Purchased" : "$" + shovelUpgradeCost;
        prestigePriceText.text = playerInventory.prestigeLevel > 0 ? "Prestiged" : "$" + prestigeCost;
        cashText.text = "Cash: $" + playerInventory.cash;

        // Button interactable state
        shovelButton.interactable = !playerInventory.hasUpgradedShovel && playerInventory.cash >= shovelUpgradeCost;
        prestigeButton.interactable = playerInventory.prestigeLevel == 0 && playerInventory.cash >= prestigeCost;
    }
}
