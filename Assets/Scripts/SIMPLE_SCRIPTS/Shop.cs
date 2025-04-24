using UnityEngine;
using TMPro;

public class Shop : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TMP_Text shovelPriceText;
    public TMP_Text prestigePriceText;
    public TMP_Text cashText; // Cash Text in Shop Panel
    private int shovelUpgradeCost = 150;
    private int prestigeCost = 300;

    private void Start()
    {
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
            UpdateUI();
        }
    }

    public void BuyPrestige()
    {
        if (playerInventory.prestigeLevel == 0 && playerInventory.cash >= prestigeCost)
        {
            playerInventory.cash -= prestigeCost;
            playerInventory.prestigeLevel = 1;
            Debug.Log("Prestige level increased!");
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        shovelPriceText.text = playerInventory.hasUpgradedShovel ? "Purchased" : "$" + shovelUpgradeCost;
        prestigePriceText.text = playerInventory.prestigeLevel > 0 ? "Prestiged" : "$" + prestigeCost;

        // Update cash text in the shop panel
        cashText.text = "Cash: $" + playerInventory.cash;
    }
}
