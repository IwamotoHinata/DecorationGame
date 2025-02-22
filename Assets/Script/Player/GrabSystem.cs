using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabSystem : MonoBehaviour
{
    private float _time = 3;
    [SerializeField] private float _initialTime = 3;

    private PlayerSound _sound;

    private bool _isRemoving = false;
    private GameObject _toRemoveObj;

    private void Start()
    {
        _time = _initialTime;

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
        if (_isRemoving) { PressTime(); }
    }
    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        Debug.Log("Grab");
        _sound.GrabSound();

        XRBaseInteractable interactable = args.interactableObject as XRBaseInteractable;
        GameObject selectedObject = interactable.gameObject;
        if (selectedObject.CompareTag("Stash"))
        {
            _isRemoving = true;
            _toRemoveObj = selectedObject;
        }
    }

    public void OnSelectExited(SelectExitEventArgs args)
    {
        Debug.Log("Release");
        _sound.ReleaseSound();
        _isRemoving = false;
        _time = _initialTime;
    }
    /// <summary>
    /// 
    /// </summary>
    public void PressTime()
    {
        _time -= Time.deltaTime;
        if (_time < 0)
        {
            Debug.Log("Remove");
            Destroy(_toRemoveObj);
            _sound.RemoveSound();
            _time = _initialTime;
            _isRemoving = false;
        }
    }
}
