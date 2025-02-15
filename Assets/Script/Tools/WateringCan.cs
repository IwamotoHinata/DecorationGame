using UnityEngine;
using UniRx;

public class WateringCan : MonoBehaviour
{
    [SerializeField] private GameObject _waterEffect;
    [SerializeField] private BoolReactiveProperty _isWatering = new BoolReactiveProperty(false);

    private bool _isBring = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _isWatering
            .Subscribe(value =>
            { 
                _waterEffect.SetActive(value);
            }).AddTo(this);
    }

    // Update is called once per frame
    void Update()
    {
        if ((this.gameObject.transform.localEulerAngles.x >= 10 && this.gameObject.transform.localEulerAngles.x <= 45) && _isWatering.Value == false)
        {
            _isWatering.Value = true;
        }
        else if((this.gameObject.transform.localEulerAngles.x > 45 && this.gameObject.transform.localEulerAngles.x < 360) && _isWatering.Value == true)
        {
            _isWatering.Value = false;
        }
    }

    public void ChangeBringBool(bool value)
    {
        _isBring = value;
    }
}
