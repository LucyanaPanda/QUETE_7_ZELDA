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
            SaveInventory.Instance.SaveTheInventory();
            SaveGlobalsVariables.Instance.SaveGlobalsData();

            _audioManager.PlaySound(AudioManager.AudioType.SavePoint);
        }
    }
}
