using UnityEngine;

public class PrestigeAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        // Check if the animator is assigned properly
        if (animator == null)
        {
            Debug.LogError("Animator component is missing!");
        }
    }

    // This method will be called from the Shop script to trigger the prestige animation
    public void TriggerPrestigeAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("PrestigeTrigger");  // Trigger the "PrestigeTrigger" trigger parameter
            Debug.Log("Prestige animation triggered!");
        }
    }

    // This method will reset the animation flag after the animation plays
    public void ResetAnimationFlag()
    {
        // No need for resetting, because trigger will automatically reset itself after being fired
        Debug.Log("Prestige animation completed.");
    }
}
