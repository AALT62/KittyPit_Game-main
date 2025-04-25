using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Automatically get the Animator component on this GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check for any WASD input
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
                        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);
        

        // This updates the 'isWalking' parameter in the Animator
        animator.SetBool("IsWalking", isMoving);
        
    }
}