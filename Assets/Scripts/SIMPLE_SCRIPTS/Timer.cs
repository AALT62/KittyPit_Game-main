using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public GameObject blockPrefab;
    public Transform spawnArea;
    public TMP_Text timerText;

    public float respawnInterval = 300f;
    public int gridWidth = 5;
    public int gridDepth = 5;
    public int gridHeight = 5;

    public float spacing = 13f; // Spacing between blocks (should be at least 12.0f)

    private float timer;
    public PlayerMovementAdvanced playerMove;
    void Start()
    {
        timer = respawnInterval;
        RespawnBlocks();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        

        if (timer <= 0f)
        {
            playerMove.PlayerRespawn();
            ClearPit();
            RespawnBlocks();
            Debug.Log("passed if");
            timer = respawnInterval;
        }
    }

    public void UpdateTimerUI()
    {
        TimeSpan time = TimeSpan.FromSeconds(timer);

        // Format as HH:MM:SS
        timerText.text = string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
    }

    void ClearPit()
    {
        foreach (Transform child in spawnArea)
        {
            Destroy(child.gameObject);
        }
    }

    void RespawnBlocks()
    {
        // Check if blockPrefab is assigned
        if (blockPrefab == null)
        {
            Debug.LogError("Block prefab is missing or destroyed.");
            return; // Exit early to avoid any further issues
        }

        Vector3 origin = spawnArea.position;

        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                for (int z = 0; z < gridDepth; z++)
                {
                    Vector3 spawnPos = origin + new Vector3(
                        x * spacing,
                        y * spacing,
                        z * spacing
                    );

                    // Instantiate block if prefab is still valid
                    GameObject newBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity);
                    newBlock.transform.parent = spawnArea;
                }
            }
        }
    }


}
