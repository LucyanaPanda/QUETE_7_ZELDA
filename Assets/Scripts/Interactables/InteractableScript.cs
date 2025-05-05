using UnityEngine;

public class InteractableScript : MonoBehaviour, IInteractable
{
    [Header("Player")]
    protected bool _playerInZone;
    protected PlayerManager _playerManager;
    protected PlayerInventory _playerInventory;

    protected AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerManager = collision.GetComponent<PlayerManager>();
        _playerInventory = collision.GetComponent<PlayerInventory>();
        if (!_playerInZone && _playerInventory!= null && _playerManager != null)
            _playerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_playerManager != null)
        {
            _playerManager = null;
            _playerInZone = false;
        }
    }

    public virtual void Interact()
    {

    }
}
