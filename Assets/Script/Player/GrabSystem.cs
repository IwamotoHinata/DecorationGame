using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class GrabSystem : MonoBehaviour
{
    private float _time = 3;
    [SerializeField] private Slider _timeSlider;

    [Header("Time it takes to throw away stash")]
    [SerializeField] private float _initialTime = 3;

    private PlayerSound _sound;
    private bool _isRemoving = false;
    private GameObject _toRemoveObj;

    private void Awake()
    {

        _time = _initialTime;
        if(_timeSlider != null)
        {
            _timeSlider.maxValue = _initialTime;
            _timeSlider.value = _initialTime;
        }
        else
        {
            Debug.LogError("_timeSlider not found");
        }

        Transform rootTransform = transform.root;
        GameObject mostParent = rootTransform.gameObject;
        _sound = mostParent.GetComponent<PlayerSound>();
        if (_sound == null)
        {
            Debug.LogError("not found PlayerSound: " + mostParent);
        }
    }

    private void Update()
    {
        if (_isRemoving) PressTime();
    }
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
         _sound?.GrabSound();

        if (args.interactableObject is XRBaseInteractable interactable)
        {
            GameObject selectedObject = interactable.gameObject;
            if (selectedObject.CompareTag("Stash"))
            {
                _isRemoving = true;
                _toRemoveObj = selectedObject;
            }
        }
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        _sound.ReleaseSound();

        _isRemoving = false;
        _time = _initialTime;
        _toRemoveObj = null;
        _timeSlider.value = _initialTime;
    }

    private void PressTime()
    {
        _time -= Time.deltaTime;
        {
            _timeSlider.value = _time;
        }
        if (_time < 0 && _toRemoveObj != null)
        {
            _sound?.RemoveSound();

            Debug.Log("Remove");
            Destroy(_toRemoveObj);
            _time = _initialTime;
            _isRemoving = false;
            _timeSlider.value = _initialTime;
        }
    }
}
