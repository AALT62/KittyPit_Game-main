using UnityEngine;

public class PanelToggleOnHold : MonoBehaviour
{
    public GameObject inventoryPanel; // Assign in Inspector
    public GameObject shopPanel;
    void Update()
    {
        if (Input.GetKey(KeyCode.B))
        {
            shopPanel.SetActive(true);
        }
        else
        {
            shopPanel.SetActive(false);
        }
        if (Input.GetKey(KeyCode.Tab))
        {
            inventoryPanel.SetActive(true);
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }
}
