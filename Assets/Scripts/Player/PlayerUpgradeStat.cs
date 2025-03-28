using UnityEngine;

public class PlayerUpgradeStat : MonoBehaviour
{
    [SerializeField] private PlayerManager _player;

   public void UseSpecialItem(Item item)
   {
        switch (item.name)
        {
            case "PowerUpAttack":
                OnUseSpecialItem(ref _player.maxAttack, item.attackBoost);
                break;
            case "PowerUpDefense":
                OnUseSpecialItem(ref _player.maxDefense, item.defBoost);
                break;
            case "PowerUpSpeed":
                OnUseSpecialItem(ref _player.maxSpeed, item.speedBoost);
                break;
            case "PowerUpHealth":
                OnUseSpecialItem(ref _player.maxHealth, item.healthBoost);
                break;
        }
    }

    public void OnUseSpecialItem(ref float stat, float upgrade)
    {
        stat += upgrade;
        _player.SavePlayerData();
    }
}
