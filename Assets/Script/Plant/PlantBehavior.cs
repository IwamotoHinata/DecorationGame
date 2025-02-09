using UnityEngine;
using System.Collections;

public class PlantBehavior : MonoBehaviour
{
    [SerializeField] private float _initialTimer = 10;
    private float _currentTimer = 0;
    private void Start()
    {
        _currentTimer = _initialTimer;
        this.enabled = false;
    }
    private void OnEnable()
    {
        StartCoroutine(WateringTimer());
    }

    private void OnDisable()
    {
        StopCoroutine(WateringTimer());
    }

    private IEnumerator WateringTimer()
    {
        while (_currentTimer > 0)
        {
            _currentTimer--;
            Debug.Log(_currentTimer);
            yield return new WaitForSeconds(1);
            if (_currentTimer == 0)
            {
                Debug.Log("Plant was wilt");
            }
        }

    }
    /// <summary>
    /// Timer recovers when watered
    /// </summary>
    /// <param name="time"></param>
    public void Watering(float time)
    {
        if (_currentTimer+time >= _initialTimer)
        {
            _currentTimer = _initialTimer;
        }
        else
        {
            _currentTimer += time;
        }
    }
}
