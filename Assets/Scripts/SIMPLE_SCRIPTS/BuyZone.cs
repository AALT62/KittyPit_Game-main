using UnityEngine;

public class BuyZone : MonoBehaviour
{
    public int dirtSellPrice = 5;  // The price for each dirt sold
    private bool isPlayerInZone = false;  // To check if player is inside the buy zone
    public Shop shop;
    [Header("References")]
    public PlayerInventory playerInventory;  // Assign in Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player entered the zone
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            Debug.Log("Player entered buy zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the player left the zone
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            Debug.Log("Player left buy zone");
        }
    }

    void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.G))  // Check if G is pressed and player is in zone
        {
            if (playerInventory != null)
            {
                playerInventory.SellDirt(dirtSellPrice);
                shop.UpdateUI();
            }
        }
    }
}
