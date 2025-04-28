using System.Threading;
using UnityEngine;

public class PlayDiggingAnimation : MonoBehaviour
{
    public Animation animationComponent;

    public string animationClipName = "Digging"; // The name of the animation clip you want to play

    private void Start()
    {
        // Get the Animation component on this GameObject
        animationComponent = GetComponent<Animation>();
    }

    private void Update()
    {
        // Check if the E key is pressed
        if (Input.GetKeyDown(KeyCode.E))
        {
            // If the Animation component is present and the animation clip exists
            if (animationComponent != null && animationComponent[animationClipName] != null)
            {
                // Play the specified animation
                animationComponent.Play(animationClipName);

                Debug.Log("E key pressed. Playing digging animation!");
            }
            else
            {
                Debug.LogWarning("Animation clip not found or Animation component missing.");
            }
        }
    }
}