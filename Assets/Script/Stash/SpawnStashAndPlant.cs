using UnityEngine;

public class SpawnStashAndPlant : MonoBehaviour
{
    [SerializeField] private GameObject _stashPrefab;
    [SerializeField] private GameObject _plantPrefab;

    [SerializeField] private Vector3 _spawnAreaSize = new Vector3(100f, 0f, 100f);
    [SerializeField] private int _stashSpawnCount = 20;
    [SerializeField] private int _plantSpawnCount = 10;
    private Vector3 _spawnPosition = Vector3.zero;

    private void Start()
    {
        SpawnPlantAndStash();
    }

    private void SpawnPlantAndStash()
    {
        var parent = this.transform;
        //Generate plant
        for (int i = 0; i < _plantSpawnCount; i++)
        {
            //Obtain the position of plant generation
            _spawnPosition = GetRandomPosition(_spawnAreaSize, this.transform.position);
            if (CheckOverlappingObject(_plantPrefab)) //if not overlapping
            {
                Instantiate(_plantPrefab, _spawnPosition, Quaternion.identity, parent);

                //Generate stash around plant
                for (int j = 0; j < _stashSpawnCount / _plantSpawnCount; j++)
                {
                    //Obtain the position of stash generation
                    _spawnPosition = GetRandomPosition(_spawnAreaSize / 10, _spawnPosition);
                    if (CheckOverlappingObject(_stashPrefab))
                    {
                        Instantiate(_stashPrefab, _spawnPosition, Quaternion.identity, parent);
                    }
                    else
                    {
                        j--;
                    }
                }
            }
            else
            {
                //if plant is overlapping,plant regeneration
                i--;
            }
        }
    }

    /// <summary>
    /// Returns a random position within the spawnAreaSize based on the center.
    /// </summary>
    /// <param name="spawnAreaSize"></param>
    /// <param name="center"></param>
    /// <returns></returns>
    private Vector3 GetRandomPosition(Vector3 spawnAreaSize, Vector3 center)
    {
        float randomX = Random.Range(center.x - spawnAreaSize.x / 2, center.x + spawnAreaSize.x / 2);
        float randomY = Random.Range(center.y - spawnAreaSize.y / 2, center.y + spawnAreaSize.y / 2);
        float randomZ = Random.Range(center.z - spawnAreaSize.z / 2, center.z + spawnAreaSize.z / 2);
        return new Vector3(randomX, randomY, randomZ);
    }

    /// <summary>
    /// Avoid overlapping Tree and plant or stash
    /// </summary>
    /// <param name="center"></param>
    /// <returns></returns>
    private bool CheckOverlappingObject(GameObject prefab)
    {
        var layerMask = LayerMask.GetMask("Tree");
        //Check overlapping
        Collider[] hitColliders = Physics.OverlapBox(_spawnPosition, prefab.transform.lossyScale, Quaternion.identity, layerMask);

        //if not overlapping
        if (hitColliders.Length == 0) { return true; }

        else { return false; }
    }
}
