using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    private CharacterController _characterController;
    private float _footSoundTime = 0.5f;
    [SerializeField] private float _initialSoundTime = 0.5f;
    private AudioManager _audioManager;
    private bool _isMoveing = false;

    private void Start()
    {
        _footSoundTime = _initialSoundTime;
        _characterController = GetComponent<CharacterController>();
        _audioManager = AudioManager.Instance;
        if (_audioManager == null)
        {
            Debug.LogError("not found AudioManager");
        }
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
                _audioManager.PlaySFX("WalkSE");
                _footSoundTime = _initialSoundTime;
            }
        }
    }
    public void GrabSound()=> _audioManager?.PlaySFX("GrabSE");
    public void ReleaseSound()=>_audioManager?.PlaySFX("ReleaseSE");
    public void RemoveSound()=> _audioManager?.PlaySFX("RemoveSE");
}
