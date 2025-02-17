using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Vector3 spawnAreaSize = new Vector3(10f, 0f, 10f);
    [SerializeField] private int spawnCount = 10;

    private void Start()
    {
        SpawnObjects();
    }

    private void SpawnObjects()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPosition = GetRandomPosition();
            Instantiate(objectToSpawn, randomPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 center = transform.position; // このスクリプトがアタッチされたオブジェクトを中心にする
        float randomX = Random.Range(center.x - spawnAreaSize.x / 2, center.x + spawnAreaSize.x / 2);
        float randomY = Random.Range(center.y - spawnAreaSize.y / 2, center.y + spawnAreaSize.y / 2);
        float randomZ = Random.Range(center.z - spawnAreaSize.z / 2, center.z + spawnAreaSize.z / 2);
        return new Vector3(randomX, randomY, randomZ);
    }
}
