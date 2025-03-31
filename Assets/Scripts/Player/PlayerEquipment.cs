using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    [Header("Equipement")]
    [SerializeField] private List<Slot> slots;
    private Item _currentWeapon;
    private Item _currentArmor;
    private Item _currentAccesorie;

    [Header("PlayerManager")]
    [SerializeField] private PlayerManager playerManager;

    private void Start()
    {
        float timer = 0;
        if (timer > 1f)
        {
            Equipement(); 
        }
        else { timer = Time.deltaTime; }
    }

    public void Equipement()
    {
       for(int i = 0;  i < slots.Count; i++)
        {
            if (slots[i].dragableItem.currentItem != null)
            {
                switch (i)
                {
                    case 0: //weapon
                        {
                            if (_currentWeapon != null)
                                playerManager.attack -= _currentWeapon.attackBoost;
                            _currentWeapon = slots[i].dragableItem.currentItem;
                            playerManager.attack += _currentWeapon.attackBoost;
                            Debug.Log("Hello");
                            break;
                        }
                    case 1: //armor
                        {
                            if (_currentArmor != null)
                                playerManager.defense -= _currentArmor.defBoost;
                            _currentArmor = slots[i].dragableItem.currentItem;
                            playerManager.defense += _currentArmor.defBoost;
                            Debug.Log("Hello1");
                            break;
                        }
                    case 2: //accesorie
                        {
                            if (_currentAccesorie != null)
                            {
                                playerManager.maxHealth -= _currentAccesorie.healthBoost;
                                playerManager.attack -= _currentAccesorie.attackBoost;
                                playerManager.defense -= _currentAccesorie.defBoost;
                                playerManager.speed -= _currentAccesorie.speedBoost;
                            }

                            _currentAccesorie = slots[i].dragableItem.currentItem;

                            playerManager.attack += _currentAccesorie.attackBoost;
                            playerManager.defense += _currentAccesorie.defBoost;
                            playerManager.speed += _currentAccesorie.speedBoost;
                            playerManager.maxHealth += _currentAccesorie.healthBoost;
                            Debug.Log("Hello2");
                            break;
                        }
                }
            }
        }
    }
}
