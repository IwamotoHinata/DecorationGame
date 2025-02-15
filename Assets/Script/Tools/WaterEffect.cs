using UnityEngine;

public class WaterEffect : MonoBehaviour
{
    public void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.CompareTag("Plant"))
        { 
            PlantStatus plantStatus = other.GetComponent<PlantStatus>();
            if (plantStatus != null)
            {
                plantStatus.IncreaseMoisture(1);
            }
        }
    }

}
