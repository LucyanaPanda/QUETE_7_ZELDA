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

    private void Start()
    {
        _inventory = GetComponentInParent<PlayerInventory>();
        _inventory.LoadInventory();
        _inventory._inventoryUi.LoadAndDisplayInventory();
        //LoadHotbar();
    }

    private void Update()
    {
        if (addedToHotbar)
        {
            //SaveHotbar();
            //addedToHotbar = false;
        }
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
                    Debug.Log("Used");

                    if (hotbar[entry.Key] <= 0)
                    {
                        PlayerInventory.inventory.Remove(entry.Key);
                        hotbar.Remove(entry.Key);
                        _hotbarUi.slots[_currentSlotSelected].dragableItem.currentItem = null;
                        Debug.Log("All Objects have been used");
                    }

                    if (entry.Key.isPotion)
                        _playerManager.ActivateBoost(entry.Key);
                    else
                        _upgradeStat.UseSpecialItem(entry.Key);

                        _hotbarUi.slots[_currentSlotSelected].UpdateInformation();
                    //SaveHotbar();
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

    //SaveLoadSystem for the HotBar, an little inventory seperated from the main one
    //In case I want to change the current inventory system

    //public void SaveHotbar()
    //{
    //    HotbarData hotbarData = new HotbarData();

    //    foreach (KeyValuePair<Item, int> entry in hotbar)
    //    {
    //        SlotData slotData = new SlotData
    //        {
    //            itemName = entry.Key.name, // Assuming item name is unique
    //            quantity = entry.Value,
    //            slotIndex = GetItemSlot(entry.Key)  // Get the UI position of the item
    //        };

    //        hotbarData.slots.Add(slotData);
    //    }

    //    string json = JsonUtility.ToJson(hotbarData);
    //    PlayerPrefs.SetString(hotbarSaveKey, json);
    //    PlayerPrefs.Save();
    //    Debug.Log("Hotbar saved");
    //}

    //public void LoadHotbar()
    //{
    //    if (PlayerPrefs.HasKey(hotbarSaveKey))
    //    {
    //        string json = PlayerPrefs.GetString(hotbarSaveKey);
    //        HotbarData hotbarData = JsonUtility.FromJson<HotbarData>(json);

    //        hotbar.Clear();

    //        foreach (SlotData slotData in hotbarData.slots)
    //        {
    //            Item item = FindItemByName(slotData.itemName);
    //            if (item != null)
    //            {
    //                hotbar[item] = slotData.quantity;
    //            }
    //        }
    //    }
    //}

    //public Item FindItemByName(string name)
    //{
    //    for (int i = 0; i < _items.Count; i++)
    //    {
    //        if (_items[i].name == name)
    //        {
    //            return _items[i];
    //        }
    //    }
    //    return null;
    //}

    //public int GetItemSlot(Item item)
    //{
    //    foreach (Slot slot in _hotbarUi.slots)
    //    {
    //        if (slot.dragableItem != null && slot.dragableItem.currentItem == item)
    //        {
    //            return slot.position;
    //        }
    //    }
    //    return -1;
    //}
}
