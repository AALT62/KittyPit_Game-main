using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int dirtValue = 1;      // How much one dirt block gives
    public int dirtCount = 0;     // Total collected dirt

    public void AddDirt(int amount)
    {
        dirtCount += amount;
        Debug.Log("Dirt collected: " + dirtCount);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
