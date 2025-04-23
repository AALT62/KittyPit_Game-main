using UnityEngine;

public class Destroy : MonoBehaviour
{
    public int blocks = 12;
    public bool colCheck = false;
    GameObject dirt;
    public GameManager gameManager;
    public int dirtValue = 1;
    private PlayerInventory playerInventory;
    public AudioClip digSound;
    private AudioSource audioSource;
    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Diggable"))
        {
            colCheck = true;
            dirt = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        colCheck = false;
    }
    void Update()
    {
        if (colCheck && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Key pressed E");

            if (dirt != null)
            {
                if (playerInventory.dirtCount < playerInventory.dirtMax)
                {

                    Destroy(dirt);
                    audioSource.PlayOneShot(digSound);
                    colCheck = false;

                    if (playerInventory != null)
                    {
                        playerInventory.dirtCount += playerInventory.dirtValue;
                        Debug.Log("Total Dirt: " + playerInventory.dirtCount);
                    }

                    blocks -= 1;

                    if (gameManager != null)
                    {
                        gameManager.WinCheck();
                    }
                }
            }
        }
    }
}