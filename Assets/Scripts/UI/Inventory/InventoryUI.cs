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
    [SerializeField] private List<Slot> _slots;
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
            DisplayInventory();
        }
    }

    public void DisplayInventory()
    {
        int index = 0;
       foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
       {
            _slots[index].image.sprite = entry.Key.image;
            _slots[index].quantityText.text = entry.Value.ToString();
            _slots[index].currentItem = entry.Key;
            index++;
       }
    }

    public void InitializeSlotsPositions()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            _slots[i].position = i;
        }
    }
}
