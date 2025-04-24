using UnityEngine;

public class EnvironmentSwitcher : MonoBehaviour
{
    // References to the different environment GameObjects
    public GameObject environment1;
    public GameObject environment2;
 // Continue for more environments if needed

    // Player inventory reference to check prestige level
    public PlayerInventory playerInventory;

    private void Start()
    {
        // Start with the first environment active
        SetEnvironment(1);
    }

    private void Update()
    {
        // Switch environment based on prestige level
        if (playerInventory.prestigeLevel == 1)  // After the player reaches prestige level 1
        {
            SetEnvironment(2);  // Show the second environment
        }
        else if (playerInventory.prestigeLevel >= 2)  // After the player reaches prestige level 2
        {
            SetEnvironment(3);  // Show the third environment
        }
    }

    private void SetEnvironment(int environmentIndex)
    {
        // Deactivate all environments first
        environment1.SetActive(false);
        environment2.SetActive(false);

        // Activate the selected environment
        switch (environmentIndex)
        {
            case 1:
                environment1.SetActive(true);
                break;
            case 2:
                environment2.SetActive(true);
                break;
            default:
                break;
        }
    }
}
