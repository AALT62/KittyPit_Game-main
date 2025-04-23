using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int dirtValue = 1;      // How much one dirt block gives
    public int dirtCount = 0;
    public int dirtMax = 5;// Total collected dirt
    public int cash = 0;          // Player's cash

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
            // Sell as much dirt as possible and earn cash
            int dirtSold = dirtCount;
            int earnedCash = dirtSold * sellPricePerDirt;
            cash += earnedCash;  // Add cash earned from selling dirt
            dirtCount = 0;  // Empty the dirt inventory

            Debug.Log("Sold " + dirtSold + " dirt for $" + earnedCash + " | Current Cash: " + cash);
        }
        else
        {
            Debug.Log("No dirt to sell!");
        }
    }

    void Start()
    {
        // Optional: Start with a specific cash value
        cash = 100;
    }
}
