using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

public class PlantStatus : MonoBehaviour
{
    [SerializeField] private IntReactiveProperty _health = new IntReactiveProperty(100);
    [SerializeField] private IntReactiveProperty _moisture = new IntReactiveProperty(100);
    [SerializeField] private ReactiveProperty<PlantState> _state = new ReactiveProperty<PlantState>(PlantState.GoodHealth);

    [SerializeField] private float _searchRadius = 1.0f;
    [SerializeField] private GameObject _effect;
    float _lastMoisture;
    GameObject _spawnedEffect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health.
            Subscribe(x =>
            {
                if (x <= 0 && _state.Value == PlantState.GoodHealth)
                {
                    _state.Value = PlantState.Wither;
                }

                if (x >= 20 && _state.Value == PlantState.Wither)
                {
                    _state.Value = PlantState.GoodHealth;
                }
            }).AddTo(this);

        _moisture
            .Skip(1)
            .Subscribe(x =>
            {
                if (x <= 0 && _state.Value == PlantState.GoodHealth)
                { 
                    _state.Value = PlantState.Wilt;
                }


                if (x >= 20 && _state.Value == PlantState.Wilt)
                {
                    _state.Value = PlantState.GoodHealth;
                }

                if (x == 100 && _spawnedEffect == null)
                {
                    _spawnedEffect = Instantiate(_effect, this.gameObject.transform.position, _effect.transform.rotation);
                    Destroy(_spawnedEffect, 5);
                }

            }).AddTo(this);

        _state
            .Skip(1)
            .Subscribe(state =>
            {
                if (state != PlantState.GoodHealth)
                {
                    ScoreManager.Instance.DecreaseScore(10);
                }
                else
                {
                    ScoreManager.Instance.IncreaseScore(15);
                }

            }).AddTo(this);

        StartCoroutine(LostMoisture());
        StartCoroutine(SearchTrash());
        
    }

    public void IncreaseMoisture(int value)
    {
        _lastMoisture = _moisture.Value;
        _moisture.Value = Mathf.Min(_moisture.Value += value, 100);
    }

    public void IncreaseHealth(int value)
    {
        _health.Value = Mathf.Min(_health.Value += value, 100);
    }

    public void DecreaseHealth(int value)
    {
        _health.Value = Mathf.Max(_health.Value -= value, 0);
    }

    public IEnumerator IncreaseHealthInGarden()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            IncreaseHealth(10);
        }
    }

    public IEnumerator LostMoisture()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            _lastMoisture = _moisture.Value;
            _moisture.Value--;
            _moisture.Value = Mathf.Max(0, _moisture.Value);
        }
    }

    public IEnumerator SearchTrash()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Collider[] colliders = new Collider[10];
            Physics.OverlapSphereNonAlloc(this.gameObject.transform.position, _searchRadius, colliders);

            

            foreach (Collider collider in colliders)
            {
                if (collider == null)
                    continue;

                if (collider.gameObject.CompareTag("Trash"))
                {
                    DecreaseHealth(10);
                    break;
                }
            }
        }
    }
}
