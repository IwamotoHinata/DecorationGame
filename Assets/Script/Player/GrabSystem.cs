using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class GrabSystem : MonoBehaviour
{
    private float _time = 3;
    [SerializeField] private Slider _timeSlider;
    [SerializeField] private Canvas _canvas;

    [Header("Time it takes to throw away trash")]
    [SerializeField] private float _initialTime = 3;

    private PlayerSound _sound;
    private bool _isRemoving = false;
    private GameObject _toRemoveObj;

    private void Awake()
    {
        _canvas.enabled = false;
        _time = _initialTime;
        if (_timeSlider != null)
        {
            _timeSlider.maxValue = _initialTime;
            _timeSlider.value = _initialTime;
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
        if (_isRemoving) RemoveTimer();
    }
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        _sound?.GrabSound();

        if (args.interactableObject is XRBaseInteractable interactable)
        {
            GameObject selectedObject = interactable.gameObject;
            if (selectedObject.CompareTag("Trash"))
            {
                _canvas.enabled = true;
                _isRemoving = true;
                _toRemoveObj = selectedObject;
            }
        }
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        _sound.ReleaseSound();

        _canvas.enabled = false;
        _isRemoving = false;
        _time = _initialTime;
        _toRemoveObj = null;
        _timeSlider.value = _initialTime;
    }

    private void RemoveTimer()
    {
        _time -= Time.deltaTime;
        {
            _timeSlider.value = _time;
        }
        if (_time < 0 && _toRemoveObj != null)
        {
            _sound?.RemoveSound();

            Debug.Log("Remove");
            ScoreManager.Instance.IncreaseScore(5);
            Destroy(_toRemoveObj);
            _time = _initialTime;
            _isRemoving = false;
            _timeSlider.value = _initialTime;
            _canvas.enabled = false;
        }
    }
}
