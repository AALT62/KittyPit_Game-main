using UnityEngine;

public class PrestigeAnimatorController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component is missing!");
        }
    }

    public void TriggerPrestigeAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("PrestigeTrigger");  // Animation for Prestige Level 1
            Debug.Log("Prestige level 1 animation triggered!");
        }
    }

    public void TriggerPrestige2Animation()
    {
        if (animator != null)
        {
            animator.SetTrigger("PrestigeLevel2Trigger");  // Animation for Prestige Level 2
            Debug.Log("Prestige level 2 animation triggered!");
        }
    }

    public void ResetAnimationFlags()
    {
        // You don't actually need to reset triggers if they're one-time triggers,
        // but if you want to manually reset, you can use ResetTrigger:
        animator.ResetTrigger("PrestigeTrigger");
        animator.ResetTrigger("PrestigeLevel2Trigger");

        Debug.Log("Animation triggers reset.");
    }
}
