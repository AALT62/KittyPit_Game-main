using UnityEngine;
using TMPro;
using UnityEngine.UI;  // For Slider UI

public class Shop : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public TMP_Text shovelPriceText;
    public TMP_Text prestigePriceText;
    public TMP_Text parrotPriceText;
    public TMP_Text cashText; // Cash Text in Shop Panel
    public TMP_Text backpackPriceText;

    public Button shovelButton;
    public Button prestigeButton;
    public Button parrotButton;
    public Button backpackButton;

    public AudioClip shovelSound;  // Sound effect for buying shovel
    public AudioClip prestigeSound;  // Sound effect for buying prestige
    public AudioClip parrotSound; // Sound effect for buying parrot
    private AudioSource audioSource;
    public PlayerMovementAdvanced playerMove;
    private int shovelUpgradeCost = 150;
    private int prestigeCost = 300;
    private int prestigeLevel2Cost = 600; // Example cost for reaching prestige level 2
    private int parrotCost = 500;
    private int backpackCost = 250;
    public Animator parrotAnimator;  // Reference to Parrot Animator

    public PrestigeAnimatorController prestigeAnimatorLevel1;  // Reference for level 1 animation
    public PrestigeAnimatorController prestigeAnimatorLevel2;  // Reference for level 2 animation
    public PrestigeAnimatorController CashSold;  // Reference for Cash Sold animation

    private void Start()
    {
        UpdateUI();
        audioSource = GetComponent<AudioSource>();

        if (playerInventory == null)
        {
            playerInventory = FindObjectOfType<PlayerInventory>();
            if (playerInventory == null)
            {
                Debug.LogError("PlayerInventory not found! Assign it in the Inspector or make sure it's in the scene.");
            }
        }

        if (playerMove == null)
        {
            playerMove = FindObjectOfType<PlayerMovementAdvanced>();
        }

        // Ensure the PrestigeAnimatorControllers are assigned in the inspector
        if (prestigeAnimatorLevel1 == null)
        {
            Debug.LogError("PrestigeAnimatorController for level 1 is not assigned!");
        }

        if (prestigeAnimatorLevel2 == null)
        {
            Debug.LogError("PrestigeAnimatorController for level 2 is not assigned!");
        }

        UpdateUI();
    }

    public void BuyShovelUpgrade()
    {
        if (!playerInventory.hasUpgradedShovel && playerInventory.cash >= shovelUpgradeCost)
        {
            playerInventory.cash -= shovelUpgradeCost;
            playerInventory.hasUpgradedShovel = true;
            playerInventory.SwitchShovel();
            playerInventory.holdTime = 2f; // Apply effect for shovel upgrade
            Debug.Log("Shovel upgraded!");

            audioSource.PlayOneShot(shovelSound);
            UpdateUI();
        }
    }

    public void BuyPrestige()
    {
        if (playerInventory.prestigeLevel == 0 && playerInventory.cash >= prestigeCost)
        {
            playerInventory.cash -= prestigeCost;
            playerInventory.prestigeLevel = 1;

            playerMove.PlayerRespawn();
            playerInventory.SwitchEnvironment();
            playerInventory.prestigeIcon1.SetActive(false);
            playerInventory.prestigeIcon2.SetActive(true);
            Debug.Log("Prestige level increased to 1!");
            audioSource.PlayOneShot(prestigeSound);

            // Trigger the level 1 prestige animation
            prestigeAnimatorLevel1.TriggerPrestigeAnimation();

            UpdateUI();
        }
        else if (playerInventory.prestigeLevel == 1 && playerInventory.cash >= prestigeLevel2Cost)
        {
            playerInventory.cash -= prestigeLevel2Cost;
            playerInventory.prestigeLevel = 2;

            playerMove.PlayerRespawn();
            playerInventory.SwitchEnvironment();
            playerInventory.prestigeIcon2.SetActive(false);
            playerInventory.prestigeIcon3.SetActive(true);
            Debug.Log("Prestige level increased to 2!");
            audioSource.PlayOneShot(prestigeSound);

            // Trigger the level 2 prestige animation
            prestigeAnimatorLevel2.TriggerPrestige2Animation();

            UpdateUI();
        }
    }
    public void BuyBackpack()
    {
        if (!playerInventory.hasBackpack&& playerInventory.prestigeLevel >= 1 && playerInventory.cash >= backpackCost)
        {
            playerInventory.cash -= backpackCost;
            playerInventory.hasBackpack = true;

            if (playerInventory.backpack != null)
            {
                playerInventory.backpack.SetActive(true);
            }
            Debug.Log("backpack purchased!");
            playerInventory.dirtMax = 10;
            
            UpdateUI();
        }
    }
    public void BuyParrot()
    {
        if (!playerInventory.hasParrot && playerInventory.prestigeLevel >= 2 && playerInventory.cash >= parrotCost)
        {
            playerInventory.cash -= parrotCost;
            playerInventory.hasParrot = true;

            if (playerInventory.parrot != null)
            {
                playerInventory.parrot.SetActive(true);
            }
            if (parrotAnimator != null)
            {
                parrotAnimator.SetTrigger("ParrotAppears");  // This assumes you set a trigger in your Animator named "ParrotAppears"
            }
            Debug.Log("Parrot purchased!");

            audioSource.PlayOneShot(parrotSound);
            UpdateUI();
        }
    }

    public void RefreshUIFromOutside()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (playerInventory.hasUpgradedShovel)
        {
            shovelPriceText.text = "Purchased";
            shovelButton.interactable = false;
        }
        else
        {
            shovelPriceText.text = "$" + shovelUpgradeCost;
            shovelButton.interactable = playerInventory.cash >= shovelUpgradeCost;
        }

        if (playerInventory.prestigeLevel == 0)
        {
            prestigePriceText.text = "$" + prestigeCost;
            prestigeButton.interactable = playerInventory.cash >= prestigeCost;
        }
        else if (playerInventory.prestigeLevel == 1)
        {
            prestigePriceText.text = "$" + prestigeLevel2Cost;
            prestigeButton.interactable = playerInventory.cash >= prestigeLevel2Cost;
        }
        else
        {
            prestigePriceText.text = "Prestiged";
            prestigeButton.interactable = false;
        }

        if (playerInventory.hasParrot)
        {
            parrotPriceText.text = "Owned";
            parrotButton.interactable = false;
        }
        else
        {
            parrotPriceText.text = "$" + parrotCost;
            parrotButton.interactable = playerInventory.prestigeLevel >= 2 && playerInventory.cash >= parrotCost;
        }
        // BACKPACK
        if (playerInventory.hasBackpack)
        {
            backpackPriceText.text = "Owned";
            backpackButton.interactable = false;
        }
        else
        {
            backpackPriceText.text = "$" + backpackCost;
            // Only enable if Prestige 2 and enough cash
            backpackButton.interactable = playerInventory.prestigeLevel >= 1 && playerInventory.cash >= backpackCost;
        }


        cashText.text = "Cash: $" + playerInventory.cash;
    }
}
