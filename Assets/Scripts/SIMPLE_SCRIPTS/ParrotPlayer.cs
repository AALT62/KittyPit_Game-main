using UnityEngine;

public class ParrotPlayer : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public PlayerMovementAdvanced playerMove;

    public AudioClip parrotSound;     // The sound to play when the player jumps
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(playerMove.jumpKey) && playerMove.readyToJump && playerMove.grounded)
        {
            if (playerInventory.hasParrot)
            {
                audioSource.PlayOneShot(parrotSound, 2f);
            }

        }
        
        if (Input.GetKeyDown(KeyCode.P)) audioSource.PlayOneShot(parrotSound, 2f);
    }
}
