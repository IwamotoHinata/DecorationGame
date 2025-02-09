using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlantingZone : MonoBehaviour
{
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Cast interactableObject to XRBaseInteractable
        XRBaseInteractable interactable = args.interactableObject as XRBaseInteractable;
        if (interactable != null)
        {
            // Get the GameObject of the selected object
            GameObject selectedObject = interactable.gameObject;

            // Check if the selected object has the PlantBehavior component attached.
            if (selectedObject.TryGetComponent(out PlantBehavior plantBahavior))
            {
                plantBahavior.enabled = true;
            }
        }
    }
    public void OnSelectExited(SelectExitEventArgs args)
    {
        XRBaseInteractable interactable = args.interactableObject as XRBaseInteractable;
        if (interactable != null)
        {
            // Get the GameObject of the selected object
            GameObject selectedObject = interactable.gameObject;

            // Check if the selected object has the PlantBehavior component attached.
            if (selectedObject.TryGetComponent(out PlantBehavior plantBahavior))
            {
                plantBahavior.enabled = false;
            }
        }
    }
}
