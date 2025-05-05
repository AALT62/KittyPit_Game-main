using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory & Progression")]
    public int dirtValue = 1;
    public int dirtCount = 0;
    public int dirtMax = 5;
    public int cash = 0;
    public int prestigeLevel = 0;
    public bool hasUpgradedShovel = false;
    public float holdTime = 3f;
    public bool hasParrot = false;
    public float jumpBoost = 0.5f;

    [Header("References")]
    public Shop shop;
    public GameObject currentShovel;
    public GameObject upgradedShovel;
    public GameObject parrot;
    public GameObject currentEnvironment;
    public GameObject prestigeEnvironment;
    public GameObject thirdEnvironment;

    [Header("UI")]
    public TMP_Text shovelStatusText;
    public TMP_Text prestigeStatusText;
    public TMP_Text dirtText;
    public TMP_Text cashText;
    public TMP_Text cashText1;
    [Header("Animation")]
    public Animator playerAnimator;
    public GameObject prestigeIcon1;  // Prestige Icon for level 1
    public GameObject prestigeIcon2;  // Prestige Icon for level 2
    public GameObject prestigeIcon3;  // Prestige Icon for level 3

    void Start()
    {
        cash = 100;
        UpdateInventoryUI();
        // Setup environments
        SwitchEnvironment();

        // Setup parrot visibility
        if (parrot != null)
            parrot.SetActive(hasParrot);

        // Setup correct shovel model
        ApplyShovelState();

        // Update UI
        UpdateInventoryUI();
        // Optionally, if you want to hide all icons at the start:
        prestigeIcon1.SetActive(true);
        prestigeIcon2.SetActive(false);
        prestigeIcon3.SetActive(false);
    }

    public void AddDirt(int amount)
    {
        dirtCount += amount;
        cash += amount * dirtValue;
        Debug.Log("Dirt collected: " + dirtCount + " | Cash: " + cash);
        UpdateInventoryUI();
    }

    public void SellDirt(int sellPricePerDirt)
    {
        if (dirtCount <= 0)
        {
            Debug.Log("No dirt to sell!");
            return;
        }

        int dirtSold = dirtCount;
        int earnedCash = dirtSold * sellPricePerDirt;

        // Update visible UI first (before bonus)
        cashText1.text = "Cash: $" + earnedCash.ToString();
        Debug.Log("Cash UI Updated: $" + earnedCash);

        // Apply prestige multiplier
        if (prestigeLevel > 0)
        {
            earnedCash *= 2;
            Debug.Log("Cash earned with prestige bonus: $" + earnedCash);
        }

        cash += earnedCash;
        dirtCount = 0;

        Debug.Log($"Sold {dirtSold} dirt for ${earnedCash}. Current Cash: ${cash}");
        UpdateInventoryUI();
    }

    public void SwitchEnvironment()
    {
        if (currentEnvironment != null)
            currentEnvironment.SetActive(prestigeLevel == 0);
        if (prestigeEnvironment != null)
            prestigeEnvironment.SetActive(prestigeLevel == 1);
        if (thirdEnvironment != null)
            thirdEnvironment.SetActive(prestigeLevel >= 2);
    }

    public void SwitchShovel()
    {
        hasUpgradedShovel = true;
        ApplyShovelState();
        UpdateInventoryUI();

        // Debug log before setting trigger
        Debug.Log("SwitchShovel triggered!");

        // Play shovel upgrade animation
        if (playerAnimator != null)
        {
            playerAnimator.SetTrigger("UpgradeShovel");
            Debug.Log("UpgradeShovel animation triggered");
        }
        else
        {
            Debug.LogError("Player Animator is not assigned!");
        }

        Debug.Log("Shovel upgraded and model switched!");
    }


    private void ApplyShovelState()
    {
        if (currentShovel != null)
            currentShovel.SetActive(!hasUpgradedShovel);
        if (upgradedShovel != null)
            upgradedShovel.SetActive(hasUpgradedShovel);

        holdTime = hasUpgradedShovel ? 2f : 3f;
        Debug.Log("Hold time set to: " + holdTime);
    }

    public void UpdateInventoryUI()
    {
        // Shovel status
        if (shovelStatusText != null)
            shovelStatusText.text = hasUpgradedShovel ? "Shovel: Upgraded" : "Shovel: Standard";

        // Prestige status
        if (prestigeStatusText != null)
            prestigeStatusText.text = prestigeLevel > 0 ? $"Prestige Level: {prestigeLevel}" : "Prestige Level: None";

        // Update Dirt and Cash UI
        if (dirtText != null)
            dirtText.text = "Dirt: " + dirtCount;

        if (cashText != null)
        {
            cashText.text = "Cash: $" + cash;
            Debug.Log("Cash UI Updated: $" + cash);
        }

        // Update the correct Prestige Icon based on prestige level
        
    }

}
