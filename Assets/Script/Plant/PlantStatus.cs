using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System.Collections;

public class PlantStatus : MonoBehaviour
{
    [SerializeField] private IntReactiveProperty _health = new IntReactiveProperty(100);
    [SerializeField] private IntReactiveProperty _moisture = new IntReactiveProperty(5);
    [SerializeField] private ReactiveProperty<PlantState> _state = new ReactiveProperty<PlantState>(PlantState.GoodHealth);

    [SerializeField] private float _searchRadius = 1.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _health.
            Subscribe(x =>
            { 
                if(x <= 0 && _state.Value == PlantState.GoodHealth)
                    _state.Value = PlantState.Wither;

                if (x >= 20 && _state.Value == PlantState.Wither)
                    _state.Value = PlantState.GoodHealth;

            }).AddTo(this);

        _moisture.
            Subscribe(x =>
            {
                if (x <= 0 && _state.Value == PlantState.GoodHealth)
                    _state.Value = PlantState.Wilt;

                if (x >= 20 && _state.Value == PlantState.Wilt)
                    _state.Value = PlantState.GoodHealth;

            }).AddTo(this);

        _state.
            Subscribe(state =>
            {
                if (state != PlantState.GoodHealth)
                {
                    ScoreManager.Instance.DecreaseScore(5);
                }

            }).AddTo(this);

        StartCoroutine(LostMoisture());
        StartCoroutine(SearchTrash());
    }

    public void IncreaseMoisture(int value)
    {
        _moisture.Value = Mathf.Min(_moisture.Value += value, 100);
    }

    public void DecreaseHealth(int value)
    {
        _health.Value = Mathf.Max(_health.Value -= value, 0);
    }

    private IEnumerator LostMoisture()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Mathf.Max(0f, _moisture.Value--);
        }
    }

    private IEnumerator SearchTrash()
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
                }
            }
        }
    }
}
