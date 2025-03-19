using NUnit.Framework;
using System.Collections.Generic;
using System.Resources;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private GameObject _inventoryPanel;
    public List<Slot> slots;
    public bool inventoryVisible = false;

    [Header("PauseManager")]
    [SerializeField] private PauseManager _pauseManager;

    private void Start()
    {
        InitializeSlotsPositions();
    }

    public void ShowHideInventory(InputAction.CallbackContext context)
    {
        if (inventoryVisible)
        {
            _inventoryPanel.SetActive(false);
            inventoryVisible = false;
            _playerInventory.SaveInventory();
            _pauseManager.ResumeGame();
            
        }
        else
        {
            _inventoryPanel.SetActive(true);
            inventoryVisible = true;
            _pauseManager.PauseGame();
            _playerInventory.LoadInventory();
            LoadAndDisplayInventory();
        }
    }

    //public void DisplayInventory()
    //{
    //    int index = 0;
    //    foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
    //    {
    //        _slots[index].image.sprite = entry.Key.image;
    //        _slots[index].quantityText.text = entry.Value.ToString();
    //        _slots[index].dragableItem.currentItem = entry.Key;
    //        index++;
    //    }
    //}

    public void InitializeSlotsPositions()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].position = i;
        }
    }

    public void LoadAndDisplayInventory()
    {
        List<int> occupiedSlots = new List<int>();

        foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
        {
            int slotIndex = GetSavedSlotIndex(entry.Key);

            if (slotIndex == -1 || slotIndex >= slots.Count || occupiedSlots.Contains(slotIndex))
            {
                slotIndex = GetNextAvailableSlot(occupiedSlots);
            }

            occupiedSlots.Add(slotIndex);

            slots[slotIndex].image.sprite = entry.Key.image;
            slots[slotIndex].quantityText.text = entry.Value.ToString();
            slots[slotIndex].dragableItem.currentItem = entry.Key;
        }
    }

    private int GetSavedSlotIndex(Item item)
    {
        string json = PlayerPrefs.GetString(PlayerInventory.inventorySaveKey);
        InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);

        if (inventoryData != null )
        {
            foreach (SlotData slotData in inventoryData.slots)
            {
                if (slotData.itemName.Equals(item.name))
                {
                    return slotData.slotIndex;
                }
            }
        }
        return -1;  // Return -1 if not found
    }

    private int GetNextAvailableSlot(List<int> occupiedSlots)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (!occupiedSlots.Contains(i))
                return i;
        }
        return 0;
    }

}
