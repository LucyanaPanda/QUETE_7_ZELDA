using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.EventSystems.EventTrigger;

public class PlayerHotbar : MonoBehaviour
{
    [Header("Hotbar")]
    public static Dictionary<Item, int> hotbar = new Dictionary<Item, int>();
    public List<Item> _items = new List<Item>();
    public static readonly string hotbarSaveKey = "player_hotbar";
    public static bool addedToHotbar;
    [SerializeField] private HotbarUI _hotbarUi;
    [SerializeField] private Transform _hotbarSlotSelected;
    [SerializeField] private int _currentSlotSelected;

    [Header("Player")]
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private PlayerUpgradeStat _upgradeStat;

    [Header("SFX")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _potionItemClip;

    private void Start()
    {
        _inventory = GetComponentInParent<PlayerInventory>();
    }


    public static void AddItem(Item item, int quantity)
    {
        hotbar.Add(item, quantity);
        addedToHotbar = true;
    }

    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
            {
                if (entry.Key == _hotbarUi.slots[_currentSlotSelected].dragableItem.currentItem && entry.Key.canBeUse)
                {
                    PlayerInventory.inventory[entry.Key] = entry.Value - 1;
                    hotbar[entry.Key] = entry.Value - 1;

                    if (hotbar[entry.Key] <= 0)
                    {
                        PlayerInventory.inventory.Remove(entry.Key);
                        hotbar.Remove(entry.Key);
                        _hotbarUi.slots[_currentSlotSelected].dragableItem.currentItem = null;
                    }

                    if (entry.Key.isPotion)
                        _playerManager.ActivateBoost(entry.Key);
                    else
                        _upgradeStat.UseSpecialItem(entry.Key);

                    _audioSource.clip = _potionItemClip;
                    _audioSource.Play();

                    _hotbarUi.slots[_currentSlotSelected].UpdateInformation();
                    _inventory.SaveInventory();
                    break;
                }
            }
        }
    }


    //Scrolling system
    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _currentSlotSelected += (int)context.ReadValue<float>();
            _currentSlotSelected = (_currentSlotSelected + 4) % 4;
            ChangeSelectedSlot();
        }
    }

    private void ChangeSelectedSlot()
    {
        _hotbarSlotSelected.SetParent(_hotbarUi.slots[_currentSlotSelected].transform);
        _hotbarSlotSelected.localPosition = Vector3.zero;
    }
}
