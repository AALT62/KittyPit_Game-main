using UnityEngine;
using TMPro;  // To use TMP_Text

public class PlayerInventory : MonoBehaviour
{
    public int dirtValue = 1;      // How much one dirt block gives
    public int dirtCount = 0;
    public int dirtMax = 5;        // Total collected dirt
    public int cash = 0;           // Player's cash
    public int prestigeLevel = 0;  // Increases over time
    public bool hasUpgradedShovel = false;
    public float holdTime = 3f;    // Hold time for digging, this will change with shovel upgrade
    public Shop shop; // Assign this in the Inspector

    // UI Text references for updating the inventory panel
    [Header("UI References")]
    public TMP_Text shovelStatusText;  // Text for displaying shovel status
    public TMP_Text prestigeStatusText; // Text for displaying prestige status
    public TMP_Text dirtText;           // Text for displaying dirt count
    public TMP_Text cashText;           // Text for displaying cash

    [Header("Environment Switching")]
    public GameObject currentEnvironment;
    public GameObject prestigeEnvironment;
    public GameObject currentShovel;
    public GameObject upgradedShovel;

    // Add dirt to inventory and cash
    public void AddDirt(int amount)
    {
        dirtCount += amount;
        cash += amount * dirtValue;  // Earn cash based on dirt collected
        Debug.Log("Dirt collected: " + dirtCount + " | Cash: " + cash);
        UpdateInventoryUI();  // Update inventory UI after adding dirt
    }

    // Sell dirt at the buy zone
    public void SellDirt(int sellPricePerDirt)
    {
        if (dirtCount > 0)
        {
            int dirtSold = dirtCount;
            int earnedCash = dirtSold * sellPricePerDirt;

            // Prestige bonus: double the cash
            if (prestigeLevel > 0)
            {
                earnedCash *= 2;
            }

            cash += earnedCash;
            dirtCount = 0;

            Debug.Log("Sold " + dirtSold + " dirt for $" + earnedCash + " | Current Cash: " + cash);
            UpdateInventoryUI();  // Update inventory UI after selling dirt
        }
        else
        {
            Debug.Log("No dirt to sell!");
        }
    }

    // Called by the Shop when prestige is bought
    public void SwitchEnvironment()
    {
        if (currentEnvironment != null)
        {
            currentEnvironment.SetActive(false);
        }
        if (prestigeEnvironment != null)
        {
            prestigeEnvironment.SetActive(true);
        }
    }

    public void SwitchShovel()
    {
        if (currentShovel != null)
        {
            currentShovel.SetActive(false);
        }
        if (upgradedShovel != null)
        {
            upgradedShovel.SetActive(true);
        }
        hasUpgradedShovel = true;  // Update the shovel status to upgraded
        UpdateInventoryUI();  // Update the inventory UI
    }

    void Start()
    {
        cash = 100;

        // Ensure only one environment is active at start
        if (prestigeLevel == 0)
        {
            if (currentEnvironment != null)
            {
                currentEnvironment.SetActive(true);
            }
            if (prestigeEnvironment != null)
            {
                prestigeEnvironment.SetActive(false);
            }
        }
        else
        {
            if (currentEnvironment != null)
            {
                currentEnvironment.SetActive(false);
            }
            if (prestigeEnvironment != null)
            {
                prestigeEnvironment.SetActive(true);
            }
        }

        if (hasUpgradedShovel == false)
        {
            if (currentShovel != null)
            {
                currentShovel.SetActive(true);
            }
            if (upgradedShovel != null)
            {
                upgradedShovel.SetActive(false);
            }
        }
        else
        {
            if (currentShovel != null)
            {
                currentShovel.SetActive(false);
            }
            if (upgradedShovel != null)
            {
                upgradedShovel.SetActive(true);
            }
        }

        // Initial UI update at start
        UpdateInventoryUI();
    }

    // Update inventory UI with the latest player data
    public void UpdateInventoryUI()
    {
        if (shovelStatusText != null)
        {
            shovelStatusText.text = "Shovel: " + (hasUpgradedShovel ? "Upgraded" : "Standard");
        }

        if (prestigeStatusText != null)
        {
            prestigeStatusText.text = "Prestige Level: " + (prestigeLevel > 0 ? prestigeLevel.ToString() : "None");
        }

        if (dirtText != null)
        {
            dirtText.text = "Dirt: " + dirtCount;
        }

        if (cashText != null)
        {
            cashText.text = "Cash: $" + cash;
        }
    }
}
