using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int dirtValue = 1;      // How much one dirt block gives
    public int dirtCount = 0;
    public int dirtMax = 5;        // Total collected dirt
    public int cash = 0;           // Player's cash
    public int prestigeLevel = 0;  // Increases over time
    public bool hasUpgradedShovel = false;
    public float holdTime = 3f;    // Hold time for digging, this will change with shovel upgrade

    // Add dirt to inventory and cash (optional)
    public void AddDirt(int amount)
    {
        dirtCount += amount;
        cash += amount * dirtValue;  // Earn cash based on dirt collected
        Debug.Log("Dirt collected: " + dirtCount + " | Cash: " + cash);
    }

    // Sell dirt at the buy zone
    public void SellDirt(int sellPricePerDirt)
    {
        if (dirtCount > 0)
        {
            // Calculate how much dirt will be sold and how much cash will be earned
            int dirtSold = dirtCount;
            int earnedCash = dirtSold * sellPricePerDirt;

            // Apply prestige bonus: Double the cash if the player has prestige level 1 or higher
            if (prestigeLevel > 0)
            {
                earnedCash *= 2;  // Double the cash for the player after selling the dirt
            }

            cash += earnedCash;  // Add cash earned from selling dirt
            dirtCount = 0;       // Empty the dirt inventory

            Debug.Log("Sold " + dirtSold + " dirt for $" + earnedCash + " | Current Cash: " + cash);

            // Example condition for increasing prestige level
            if (cash >= 1000)  // For example, when cash reaches 1000, increase prestige
            {
                prestigeLevel += 1;
                cash = 0; // Reset cash after upgrading prestige
                Debug.Log("Prestige Level Up! New Prestige: " + prestigeLevel);
            }
        }
        else
        {
            Debug.Log("No dirt to sell!");
        }
    }

    void Start()
    {
        // Optional: Start with a specific cash value
        cash = 100;  // Starting amount of cash
    }
}
