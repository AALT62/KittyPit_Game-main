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
            Cursor.visible = true;  // Make cursor visible when shop is open
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor so it can move freely
        }
        else
        {
            Time.timeScale = 1f;  // Resume the game if B is not held down
            Cursor.visible = false;  // Hide the cursor when shop is closed
            Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor back to the center of the screen
        }
    }
}
