using UnityEngine;

public class Destroy : MonoBehaviour
{
    public int blocks = 12;
    public bool colCheck = false;
    GameObject dirt;
    public GameManager gameManager;
    public int dirtValue =1;
    private PlayerInventory playerInventory;

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
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
                Destroy(dirt);
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
    /*void Update()
    {
        if (colCheck == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
               Debug.Log("Key pressed e");
               Destroy(dirt);
               
               colCheck = false;
               if (gameManager != null)
               {
                    gameManager.WinCheck();
               }
            }
        }
    }*/
}