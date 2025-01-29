using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PlantingZone : MonoBehaviour
{
    private bool _isOccupied = false;
    [SerializeField] private GameObject plantPrefab;
    [SerializeField] private Transform plantingPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") && TryGetComponent(out XRDirectInteractor interactor))
        {
            //Register to execute Plant method when controller's select buttun is pressed
            interactor.selectEntered.AddListener(Plant);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Hand") && TryGetComponent(out XRDirectInteractor interactor))
        {
            interactor.selectEntered.RemoveListener(Plant);
        }
    }

    private void Plant(SelectEnterEventArgs args)
    {
        // Get the object in the player's hand
        XRGrabInteractable grabbedObject = args.interactableObject as XRGrabInteractable;

        if (grabbedObject != null)
        {
            if (grabbedObject.CompareTag("Plant") && !_isOccupied)
            {
                // Generate where to plant
                Instantiate(plantPrefab, plantingPosition.position, Quaternion.identity);

                // Destroy plant that had
                Destroy(grabbedObject.gameObject);
                _isOccupied = true;
            }
        }
    }
}
