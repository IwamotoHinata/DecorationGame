using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlantingZone : MonoBehaviour
{
    private PlantCounter _plantCounter;
    private void Start()
    {
        _plantCounter = FindFirstObjectByType<PlantCounter>();
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // Cast interactableObject to XRBaseInteractable
        XRBaseInteractable interactable = args.interactableObject as XRBaseInteractable;
        if (interactable != null)
        {
            // Get the GameObject of the selected object
            GameObject selectedObject = interactable.gameObject;

            // Check if the selected object has the PlantBehavior component attached.
            if (selectedObject.TryGetComponent(out PlantStatus plantStatus))
            {
                plantStatus.StopAllCoroutines();
                plantStatus.StartCoroutine(plantStatus.IncreaseHealthInGarden());
                ScoreManager.Instance.IncreaseScore(5);
                _plantCounter.IncreaseCount();
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
            if (selectedObject.TryGetComponent(out PlantStatus plantStatus))
            {
                plantStatus.StartCoroutine(plantStatus.LostMoisture());
                plantStatus.StartCoroutine(plantStatus.SearchTrash());
                plantStatus.StopCoroutine(plantStatus.IncreaseHealthInGarden());
                _plantCounter.DecreaseCount();
            }
        }
    }
}
