using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Destroy destroy;

    public GameObject popupPanel;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        popupPanel.SetActive(false);
        
    }

    public void WinCheck()
    {
        Debug.Log("Checking Win Condition");
        if (destroy != null)  // Check if destroy is assigned
        {
            if (destroy.blocks <= 0)
            {
                Debug.Log("You win! All blocks destroyed.");
                
            }
        }
        else
        {
            Debug.LogError("Destroy script is not assigned in GameManager!");
        }
    }
}
