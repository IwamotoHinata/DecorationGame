using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class PlantingZone : MonoBehaviour
{
    private bool _isOccupied = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (!_isOccupied && collision.gameObject.gameObject.CompareTag("Plant"))
        {
            GameObject plant = collision.gameObject;

            XRGrabInteractable grabInteractable = plant.GetComponent<XRGrabInteractable>();
            if (grabInteractable != null)
            {
                grabInteractable.enabled = false;
            }
            Rigidbody rb = plant.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }

            plant.transform.position = transform.position + new Vector3(0, transform.localScale.y*2, 0);
            _isOccupied = true;
            Debug.Log("Planted");
        }
    }
}
