using UnityEngine;

public class Destroy : MonoBehaviour
{
    public int blocks = 12;
    public bool colCheck = false;
    GameObject dirt;
    public GameManager gameManager;
    public int dirtValue =1;
    
    private void Start()
    {
        
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
    }
}