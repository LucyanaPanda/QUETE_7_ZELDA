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
    [SerializeField] private GameObject _inventoryPanel;
    public static List<InventorySlot> _slots;
    public static bool inventoryVisible = false;

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
            _pauseManager.ResumeGame();
        }
        else
        {
            _inventoryPanel.SetActive(true);
            inventoryVisible = true;
            _pauseManager.PauseGame();
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
        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].position = i;
        }
    }

    public void LoadAndDisplayInventory()
    {
        int index = 0;
        List<int> occupiedSlots = new List<int>();

        foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
        {
            int slotIndex = GetSavedSlotIndex(entry.Key);

            if (slotIndex == -1 || slotIndex >= _slots.Count || occupiedSlots.Contains(slotIndex))
            {
                slotIndex = GetNextAvailableSlot(occupiedSlots);
            }

            occupiedSlots.Add(slotIndex);

            _slots[slotIndex].image.sprite = entry.Key.image;
            _slots[slotIndex].quantityText.text = entry.Value.ToString();
            _slots[slotIndex].dragableItem.currentItem = entry.Key;
        }
    }

    private int GetSavedSlotIndex(Item item)
    {
        // Implement a lookup to find the saved slot index for the given item
        return -1;  // Return -1 if not found
    }

    private int GetNextAvailableSlot(List<int> occupiedSlots)
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            if (!occupiedSlots.Contains(i))
                return i;
        }
        return 0;
    }

}
