using UnityEngine;
using UnityEngine.InputSystem;

public class Spawnpoint : InteractableScript, IInteractable
{
    public override void Interact()
    {
        if (_playerInZone && _playerManager != null)
        {
            _playerManager.UseSpawnpoint(transform.position);
        }
    }
}
