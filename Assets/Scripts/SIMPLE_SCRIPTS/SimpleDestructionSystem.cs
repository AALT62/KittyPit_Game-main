using UnityEngine;

public class SimpleDestructionSystem : MonoBehaviour
{
    [Header("Settings")]
    public float holdTimeRequired = 1f;
    public string destructibleTag = "Tier1Block";
    public LayerMask destructibleLayer;
    public GameObject destructionEffect;

    public float currentHoldTime;
    public GameObject currentTarget;

    void Update()
    {
        // Check for button hold
        if (Input.GetKey(KeyCode.E) && currentTarget != null)
        {
            currentHoldTime += Time.deltaTime;

            if (currentHoldTime >= holdTimeRequired)
            {
                DestroyCurrentTarget();
            }
        }
        else
        {
            currentHoldTime = 0f;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if object is destructible
        if (collision.gameObject.CompareTag(destructibleTag) &&
            ((1 << collision.gameObject.layer) & destructibleLayer) != 0)
        {
            currentTarget = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == currentTarget)
        {
            currentTarget = null;
            currentHoldTime = 0f;
        }
    }

    private void DestroyCurrentTarget()
    {
        if (currentTarget != null)
        {
            // Instantiate effect if assigned
            if (destructionEffect != null)
            {
                Instantiate(destructionEffect, currentTarget.transform.position,
                          currentTarget.transform.rotation);
            }

            Destroy(currentTarget);
            currentTarget = null;
            currentHoldTime = 0f;
        }
    }
}
