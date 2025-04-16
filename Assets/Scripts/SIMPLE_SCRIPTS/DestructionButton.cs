using UnityEngine;
using UnityEngine.EventSystems;

public class DestructionButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SimpleDestructionSystem destructionSystem;

    public void OnPointerDown(PointerEventData eventData)
    {
        destructionSystem.currentHoldTime = 0.01f; // Start counting
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        destructionSystem.currentHoldTime = 0.0f; // Start counting
    }
}


