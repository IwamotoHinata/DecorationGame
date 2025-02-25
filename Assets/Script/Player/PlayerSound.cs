using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private CharacterController _characterController;
    private float _footSoundTime = 0.5f;
    [SerializeField] private float _initialSoundTime = 0.5f;
    private bool _isMoveing = false;

    private void Start()
    {
        _footSoundTime = _initialSoundTime;
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        WalkSound();
    }

    private void WalkSound()
    {
        _isMoveing = Mathf.Abs(_characterController.velocity.x) + Mathf.Abs(_characterController.velocity.z) >= 0.05f;
        if (_isMoveing)
        {
            _footSoundTime -= Time.deltaTime;
            if (_footSoundTime < 0)
            {
                AudioManager.Instance?.PlaySFX("WalkSE");
                _footSoundTime = _initialSoundTime;
            }
        }
    }
    public void GrabSound()=> AudioManager.Instance?.PlaySFX("GrabSE");
    public void ReleaseSound()=>AudioManager.Instance?.PlaySFX("ReleaseSE");
    public void RemoveSound()=> AudioManager.Instance?.PlaySFX("RemoveSE");
}
