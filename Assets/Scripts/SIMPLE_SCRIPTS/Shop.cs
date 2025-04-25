using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting; // For Button reference

public class Shop : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TMP_Text shovelPriceText;
    public TMP_Text prestigePriceText;
    public TMP_Text cashText; // Cash Text in Shop Panel

    public Button shovelButton;
    public Button prestigeButton;

    private int shovelUpgradeCost = 150;
    private int prestigeCost = 300;

    private void Start()
    {
        // Initial check and UI update
        Debug.Log("Shop Start - Player cash: " + playerInventory.cash);
        UpdateUI();
    }

    public void BuyShovelUpgrade()
    {
        Debug.Log("Attempting to buy shovel upgrade...");

        if (!playerInventory.hasUpgradedShovel && playerInventory.cash >= shovelUpgradeCost)
        {
            playerInventory.cash -= shovelUpgradeCost;
            playerInventory.hasUpgradedShovel = true;
            playerInventory.holdTime = 2f; // Apply effect for shovel upgrade
            playerInventory.SwitchShovel();
            Debug.Log("Shovel upgraded!");
            UpdateUI();
        }
        else
        {
            Debug.Log("Can't buy shovel upgrade. Current cash: " + playerInventory.cash);
        }
    }

    public void BuyPrestige()
    {
        Debug.Log("Attempting to buy prestige...");

        if (playerInventory.prestigeLevel == 0 && playerInventory.cash >= prestigeCost)
        {
            playerInventory.cash -= prestigeCost;
            playerInventory.prestigeLevel = 1;

            // Switch to new environment
            playerInventory.SwitchEnvironment();

            Debug.Log("Prestige level increased!");
            UpdateUI();
        }
        else
        {
            Debug.Log("Can't buy prestige. Current cash: " + playerInventory.cash);
        }
    }

    public void UpdateUI()
    {
        Debug.Log("Updating UI...");
        Debug.Log("Player cash: $" + playerInventory.cash);

        // Update text labels
        string shovelPriceTextValue = playerInventory.hasUpgradedShovel ? "Purchased" : "$" + shovelUpgradeCost;
        shovelPriceText.text = shovelPriceTextValue;

        string prestigePriceTextValue = playerInventory.prestigeLevel > 0 ? "Prestiged" : "$" + prestigeCost;
        prestigePriceText.text = prestigePriceTextValue;

        cashText.text = "Cash: $" + playerInventory.cash;

        // Debug logs for button conditions
        Debug.Log("Shovel Button Interactable: " + (!playerInventory.hasUpgradedShovel && playerInventory.cash >= shovelUpgradeCost));
        Debug.Log("Prestige Button Interactable: " + (playerInventory.prestigeLevel == 0 && playerInventory.cash >= prestigeCost));

        // Button interactability logic
        shovelButton.interactable = !playerInventory.hasUpgradedShovel && playerInventory.cash >= shovelUpgradeCost;
        prestigeButton.interactable = playerInventory.prestigeLevel == 0 && playerInventory.cash >= prestigeCost;
    }
}
    