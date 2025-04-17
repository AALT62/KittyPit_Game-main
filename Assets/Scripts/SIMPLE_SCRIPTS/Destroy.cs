using UnityEngine;

public class Destroy : MonoBehaviour
{
    public bool colCheck = false;
    GameObject dirt;
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
               Destroy(dirt);
               colCheck = false;
                
            }
        }
    }
}