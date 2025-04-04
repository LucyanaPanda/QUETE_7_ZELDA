using UnityEngine;
using UnityEngine.InputSystem;

public class Spawnpoint : InteractableScript
{
    
    public override void Interact()
    {
        if (_playerInZone && _playerManager != null)
        {
            _playerManager.UseSpawnpoint(transform.position);
            _playerManager.SavePlayerData();
            _playerInventory.SaveInventory();

            _audioSource.clip = _audioclip;
            _audioSource.Play();
        }
    }
}
