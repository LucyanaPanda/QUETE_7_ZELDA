using UnityEngine;

public class InteractableScript : MonoBehaviour
{
    [Header("Player")]
    protected bool _playerInZone;
    protected PlayerManager _playerManager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerManager = collision.GetComponent<PlayerManager>();
        if (!_playerInZone && _playerManager != null)
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
