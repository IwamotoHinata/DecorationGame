using UnityEngine;

public class PlantCounter : MonoBehaviour
{
    [SerializeField] private PlantingZone[] _plantingZones;
    [SerializeField] private int _count = 0;
    private int _maxCount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _plantingZones = FindObjectsByType<PlantingZone>(FindObjectsSortMode.None);
        _maxCount = _plantingZones.Length;
    }

    public void IncreaseCount()
    {
        _count++;
        if (_count == _maxCount)
        {
            ScoreManager.Instance.IncreaseScore(25);
        }
    }

    public void DecreaseCount()
    {
        if (_count == _maxCount)
        {
            ScoreManager.Instance.DecreaseScore(25);
        }
        _count--;
    }
}
