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
    public bool hasParrot = false;

    public float jumpBoost = 0.5f; // Extra jump boost if player owns parrot
    // UI Text references for updating the inventory panel
    [Header("UI References")]
    public TMP_Text shovelStatusText;  // Text for displaying shovel status
    public TMP_Text prestigeStatusText; // Text for displaying prestige status
    public TMP_Text dirtText;           // Text for displaying dirt count
    public TMP_Text cashText;           // Text for displaying cash
    public TMP_Text cashText1;           // Text for displaying cash
    [Header("Environment Switching")]
    public GameObject currentEnvironment;
    public GameObject prestigeEnvironment;
    public GameObject thirdEnvironment;
    public GameObject currentShovel;
    public GameObject upgradedShovel;
    public GameObject parrot;
    // Add dirt to inventory and cash
    public void AddDirt(int amount)
    {
        dirtCount += amount;
        cash += amount * dirtValue;  // Earn cash based on dirt collected
        Debug.Log("Dirt collected: " + dirtCount + " | Cash: " + cash);
        UpdateInventoryUI();  // Update inventory UI after adding dirt
    }

    // Sell dirt at the buy zone
    // Sell dirt at the buy zone
    public void SellDirt(int sellPricePerDirt)
    {
        if (dirtCount > 0)
        {
            // Track how much dirt is sold
            int dirtSold = dirtCount;

            // Calculate how much cash is earned from selling dirt
            int earnedCash = dirtSold * sellPricePerDirt;
            cashText1.text = "Cash: $" + earnedCash.ToString();  // Update the cash display
            Debug.Log("Cash UI Updated: $" + cash);  // Debug log to confirm the update
            
            Debug.Log("Cash earned before prestige: $" + earnedCash);

            // Apply prestige bonus if applicable (double cash for prestige level 1 or higher)
            if (prestigeLevel > 0)
            {
                earnedCash *= 2;  // Double the cash if prestige level > 0
                Debug.Log("Cash earned with prestige bonus: $" + earnedCash);
            }

            // Add the earned cash to the player's total
            cash += earnedCash;

            // Reset the dirt count
            dirtCount = 0;

            // Debug log for current status
            Debug.Log("Sold " + dirtSold + " dirt for $" + earnedCash + " | Current Cash: $" + cash);

            // Now update UI after selling dirt
            UpdateInventoryUI();
        }
        else
        {
            Debug.Log("No dirt to sell!");
        }
    }



    // Called by the Shop when prestige is bought
    public void SwitchEnvironment()
    {
        // Disable all environments first
        if (currentEnvironment != null)
        {
            currentEnvironment.SetActive(false);
        }
        if (prestigeEnvironment != null)
        {
            prestigeEnvironment.SetActive(false);
        }
        if (thirdEnvironment != null)  // Ensure the third environment is hidden at the start
        {
            thirdEnvironment.SetActive(false);
        }

        // Switch based on prestige level
        if (prestigeLevel == 0)
        {
            if (currentEnvironment != null)
            {
                currentEnvironment.SetActive(true);
            }
        }
        else if (prestigeLevel == 1)
        {
            if (prestigeEnvironment != null)
            {
                prestigeEnvironment.SetActive(true);
            }
        }
        else if (prestigeLevel >= 2)  // When prestige level is 2 or higher
        {
            if (thirdEnvironment != null)
            {
                thirdEnvironment.SetActive(true);
            }
        }
    }



    public void SwitchShovel()
    {
        // Ensure the current shovel exists before deactivating it
        if (currentShovel != null)
        {
            currentShovel.SetActive(false); // Deactivate the current shovel model
        }

        // Ensure the upgraded shovel exists before activating it
        if (upgradedShovel != null)
        {
            upgradedShovel.SetActive(true); // Activate the upgraded shovel model
        }

        // Update the inventory to reflect the shovel upgrade
        hasUpgradedShovel = true;  // Mark that the shovel has been upgraded

        // Update UI to reflect the change in the player's inventory
        UpdateInventoryUI();

        // Optionally, log for debugging
        Debug.Log("Shovel upgraded and model switched!");
    }


    void Start()
    {
        cash = 100;

        // Ensure the parrot is inactive at the start
        if (parrot != null)
        {
            parrot.SetActive(false); // Parrot is invisible until purchased
        }

        // Ensure only one environment is active at start
        if (prestigeLevel == 0)
        {
            if (currentEnvironment != null)
            {
                UpdateInventoryUI();
                currentEnvironment.SetActive(true);
            }
            if (prestigeEnvironment != null)
            {
                UpdateInventoryUI();
                prestigeEnvironment.SetActive(false);
            }
            if (thirdEnvironment != null)
            {
                UpdateInventoryUI();
                thirdEnvironment.SetActive(false);  // Hide third environment at the start
            }
        }
        else if (prestigeLevel == 1)
        {
            if (currentEnvironment != null)
            {
                UpdateInventoryUI();
                currentEnvironment.SetActive(false);
            }
            if (prestigeEnvironment != null)
            {
                UpdateInventoryUI();
                prestigeEnvironment.SetActive(true);
            }
            if (thirdEnvironment != null)
            {
                UpdateInventoryUI();
                thirdEnvironment.SetActive(false);  // Ensure third environment is still hidden
            }
        }
        else if (prestigeLevel >= 2)
        {
            if (currentEnvironment != null)
            {
                UpdateInventoryUI();
                currentEnvironment.SetActive(false);
            }
            if (prestigeEnvironment != null)
            {
                UpdateInventoryUI();
                prestigeEnvironment.SetActive(false);
            }
            if (thirdEnvironment != null)
            {
                UpdateInventoryUI();
                thirdEnvironment.SetActive(true);  // Show third environment when prestige level is 2 or higher
            }
        }

        // Ensure the upgraded shovel is hidden at the start
        if (hasUpgradedShovel)
        {
            if (currentShovel != null)
            {
                currentShovel.SetActive(false);
            }
            if (upgradedShovel != null)
            {
                upgradedShovel.SetActive(true); // Show upgraded shovel if the player has it
            }
        }
        else
        {
            if (currentShovel != null)
            {
                currentShovel.SetActive(true); // Show standard shovel if the player hasn't upgraded
            }
            if (upgradedShovel != null)
            {
                upgradedShovel.SetActive(false); // Hide upgraded shovel at the start
            }
        }

        // Initial UI update at start
        UpdateInventoryUI();
    }



    // Update inventory UI with the latest player data
    // Update inventory UI with the latest player data
    public void UpdateInventoryUI()
    {
        if (shovelStatusText != null)
        {
            shovelStatusText.text = hasUpgradedShovel ? "Shovel: Upgraded" : "Shovel: Standard";
        }

        if (prestigeStatusText != null)
        {
            prestigeStatusText.text = prestigeLevel > 0 ? "Prestige Level: " + prestigeLevel.ToString() : "Prestige Level: None";
        }

        if (dirtText != null)
        {
            dirtText.text = "Dirt: " + dirtCount.ToString();
        }

        if (cashText != null)
        {
            cashText.text = "Cash: $" + cash.ToString();  // Update the cash display
            Debug.Log("Cash UI Updated: $" + cash);  // Debug log to confirm the update
        }
    }



}
