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

    [Header("Player")]
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private InventoryPlayerStats _playerStats;

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
       //for(int i = 0;  i < slots.Count; i++)
       // {
       //     if (slots[i].dragableItem.currentItem != null)
       //     {
       //         switch (i)
       //         {
       //             case 0: //weapon
       //                 {
       //                     if (_currentWeapon != null)
       //                         _playerManager.attack -= _currentWeapon.attackBoost;
       //                     _currentWeapon = slots[i].dragableItem.currentItem;
       //                     _playerManager.attack += _currentWeapon.attackBoost;
       //                     break;
       //                 }
       //             case 1: //armor
       //                 {
       //                     if (_currentArmor != null)
       //                         _playerManager.defense -= _currentArmor.defBoost;
       //                     _currentArmor = slots[i].dragableItem.currentItem;
       //                     _playerManager.defense += _currentArmor.defBoost;
       //                     break;
       //                 }
       //             case 2: //accesorie
       //                 {
       //                     if (_currentAccesorie != null)
       //                     {
       //                         _playerManager.maxHealth -= _currentAccesorie.healthBoost;
       //                         _playerManager.attack -= _currentAccesorie.attackBoost;
       //                         _playerManager.defense -= _currentAccesorie.defBoost;
       //                         _playerManager.speed -= _currentAccesorie.speedBoost;
       //                     }

        //                     _currentAccesorie = slots[i].dragableItem.currentItem;

        //                     _playerManager.attack += _currentAccesorie.attackBoost;
        //                     _playerManager.defense += _currentAccesorie.defBoost;
        //                     _playerManager.speed += _currentAccesorie.speedBoost;
        //                     _playerManager.maxHealth += _currentAccesorie.healthBoost;
        //                     break;
        //                 }
        //         }
        //     }
        //}

        // _playerStats.UpdateStatsUI();

        for (int i = 0; i < slots.Count; i++)
        {
            Item newItem = slots[i].dragableItem.currentItem;

            switch (i)
            {
                case 0: // Weapon
                    if (_currentWeapon != null)
                    {
                        _playerManager.attack -= _currentWeapon.attackBoost;
                    }
                    _currentWeapon = newItem;
                    if (_currentWeapon != null)
                    {
                        _playerManager.attack += _currentWeapon.attackBoost;
                    }
                    break;

                case 1: // Armor
                    if (_currentArmor != null)
                    {
                        _playerManager.defense -= _currentArmor.defBoost;
                    }
                    _currentArmor = newItem;
                    if (_currentArmor != null)
                    {
                        _playerManager.defense += _currentArmor.defBoost;
                    }
                    break;

                case 2: // Accessory
                    if (_currentAccesorie != null)
                    {
                        _playerManager.maxHealth -= _currentAccesorie.healthBoost;
                        _playerManager.attack -= _currentAccesorie.attackBoost;
                        _playerManager.defense -= _currentAccesorie.defBoost;
                        _playerManager.speed -= _currentAccesorie.speedBoost;
                    }
                    _currentAccesorie = newItem;
                    if (_currentAccesorie != null)
                    {
                        _playerManager.maxHealth += _currentAccesorie.healthBoost;
                        _playerManager.attack += _currentAccesorie.attackBoost;
                        _playerManager.defense += _currentAccesorie.defBoost;
                        _playerManager.speed += _currentAccesorie.speedBoost;

                        _playerManager.HealthChanged();
                    }
                    break;
            }
        }

        _playerStats.UpdateStatsUI();
    }

}

