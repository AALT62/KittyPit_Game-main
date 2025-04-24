using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Inventory UI")]
    public TextMeshProUGUI dirtText;
    public TextMeshProUGUI cashText;  // Display for cash

    [Header("Timer UI")]
    public TextMeshProUGUI timerText;

    [Header("References")]
    public PlayerInventory playerInventory;
    public Timer timerScript;  // Reference to the Timer script

    void Update()
    {
        UpdateDirtUI();  // Update dirt count on UI
        UpdateCashUI();  // Update cash UI
        UpdateTimerUI(); // Update timer on UI
    }

    void UpdateDirtUI()
    {
        if (playerInventory != null)
        {
            dirtText.text = "Dirt: " + playerInventory.dirtCount;
        }
    }

    void UpdateCashUI()
    {
        if (playerInventory != null)
        {
            cashText.text = "Cash: $" + playerInventory.cash;
        }
    }

    void UpdateTimerUI()
    {
        if (timerScript != null)
        {
            timerScript.UpdateTimerUI(); // Call the method from the Timer script to update the timer UI
        }
    }
}
