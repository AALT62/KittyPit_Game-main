using UnityEngine;
using TMPro;

public class BuyZone : MonoBehaviour
{
    public int dirtSellPrice = 5; // Price for each dirt sold
    private bool isPlayerInZone = false;

    [Header("References")]
    public Shop shop;
    public PlayerInventory playerInventory;
    public Animator sellAnimator;           // Animator for the sale animation
    public AudioClip dirtSellSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (sellAnimator == null)
        {
            Debug.LogError("Sell Animator not assigned!");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = true;
            Debug.Log("Player entered buy zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInZone = false;
            Debug.Log("Player left buy zone");
        }
    }

    private void Update()
    {
        if (isPlayerInZone && Input.GetKeyDown(KeyCode.G))
        {
            if (playerInventory != null)
            {
                // Store how many dirt were sold and how much was earned
                int soldAmount = playerInventory.dirtCount;
                int baseEarnings = soldAmount * dirtSellPrice;
                int totalEarnings = playerInventory.prestigeLevel > 0 ? baseEarnings * 2 : baseEarnings;

                if (soldAmount > 0)
                {
                    // Call SellDirt to do the math, UI, reset dirt count, etc.
                    playerInventory.SellDirt(dirtSellPrice);

                    
                    // Play animation
                    if (sellAnimator != null)
                    {
                        sellAnimator.SetTrigger("Sell");
                    }

                    // Play sound
                    if (audioSource != null && dirtSellSound != null)
                    {
                        audioSource.PlayOneShot(dirtSellSound);
                    }

                    // Update shop UI if needed
                    shop?.UpdateUI();
                }
                else
                {
                    Debug.Log("No dirt to sell.");
                }
            }
        }
    }
}
