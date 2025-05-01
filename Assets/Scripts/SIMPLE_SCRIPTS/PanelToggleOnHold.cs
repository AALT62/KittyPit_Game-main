using UnityEngine;
using UnityEngine.UI;  // Add this line to access UI components like Button

public class PanelToggleOnHold : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject shopPanel;
    public GameObject gameUi;
    public GameObject confirmReturnPanel;  // Panel for the confirmation prompt
    public Button returnToMainMenuButton;  // Button to return to main menu

    private float escHoldTime = 1f;        // Time to hold Esc key (1 second)
    private float escPressTime = 0f;       // Timer to track Esc hold time
    private bool isEscHeld = false;        // To track if Esc is being held down
    private bool isPanelActive = false;    // To track if the panel is open

    void Start()
    {
        // Ensure confirmReturnPanel is inactive at the start
        confirmReturnPanel.SetActive(false);
    }

    void Update()
    {
        bool isHoldingB = Input.GetKey(KeyCode.B);
        bool isHoldingTab = Input.GetKey(KeyCode.Tab);

        // Toggle UI panels
        shopPanel.SetActive(isHoldingB);
        inventoryPanel.SetActive(isHoldingTab);

        // Show game UI only when neither panel is open
        gameUi.SetActive(!isHoldingB && !isHoldingTab);

        // Pause ONLY if shop or inventory is open
        if (isHoldingB || isHoldingTab)
        {
            Time.timeScale = 0f;  // Pause the game if either panel is held down
            Cursor.visible = true;  // Make cursor visible when any panel is open
            Cursor.lockState = CursorLockMode.None;  // Unlock the cursor so it can move freely
        }
        else
        {
            Time.timeScale = 1f;  // Resume the game if no panels are open
            if (!isPanelActive)  // Ensure cursor is hidden if no panel is active
            {
                Cursor.visible = false;  // Hide the cursor when no panels are open
                Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor back to the center of the screen
            }
        }

        // Detect holding Esc key for the return panel
        if (Input.GetKey(KeyCode.Escape))
        {
            escPressTime += Time.deltaTime;  // Increment timer while holding Esc

            // If Esc has been held for the required time, show the return menu prompt
            if (escPressTime >= escHoldTime && !confirmReturnPanel.activeSelf)
            {
                isEscHeld = true;  // Mark Esc as being held
                ShowReturnPrompt();
            }
        }
        else
        {
            // If Esc key is released, reset the timer and close the panel if it was open
            escPressTime = 0f;
            if (isEscHeld)
            {
                // Close the panel if the player stops holding Esc
                confirmReturnPanel.SetActive(false);
                isPanelActive = false;  // Set panel status to inactive
                isEscHeld = false;
                Time.timeScale = 1f;  // Resume the game when panel is closed
                Cursor.visible = false;  // Hide cursor when the panel is closed
                Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor when the panel is closed
            }
        }
    }

    // Show the UI prompt to confirm returning to the main menu
    private void ShowReturnPrompt()
    {
        confirmReturnPanel.SetActive(true);
        isPanelActive = true;  // Mark the panel as active
        Time.timeScale = 0f;  // Pause the game when the panel is open
        Cursor.visible = true;  // Make cursor visible when the return panel is open
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor so it can move freely
        Debug.Log("Hold Esc for 1 second to return to the main menu.");
    }

    // Method to return to the main menu when the player confirms
    public void OnReturnToMainMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");
    }

    // Optional: Method to cancel returning to the main menu
    public void CancelReturnToMainMenu()
    {
        confirmReturnPanel.SetActive(false);
        isPanelActive = false;  // Mark the panel as inactive
        Time.timeScale = 1f;  // Resume the game when the player cancels the return
        Cursor.visible = false;  // Hide cursor when the panel is closed
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor when the panel is closed
    }
}
