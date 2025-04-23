using UnityEngine;

public class PanelToggleOnHold : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject shopPanel;
    public GameObject gameUi;

    void Update()
    {
        bool isHoldingB = Input.GetKey(KeyCode.B);
        bool isHoldingTab = Input.GetKey(KeyCode.Tab);

        // Toggle UI panels
        shopPanel.SetActive(isHoldingB);
        inventoryPanel.SetActive(isHoldingTab);

        // Show game UI only when neither panel is open
        gameUi.SetActive(!isHoldingB && !isHoldingTab);

        // Pause ONLY if shop is open
        if (isHoldingB)
        {
            Time.timeScale = 0f;  // Pause the game if B is held down
        }
        else
        {
            Time.timeScale = 1f;  // Resume the game if B is not held down
        }
    }
}
