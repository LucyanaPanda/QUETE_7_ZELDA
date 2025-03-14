using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private List<Image> _slots;
    private TMP_Text _quantityText;
    private bool _inventoryVisible = false;

    [Header("PauseManager")]
    [SerializeField] private PauseManager _pauseManager;

    public void ShowHideInventory(InputAction.CallbackContext context)
    {
        if (_inventoryVisible)
        {
            _inventoryPanel.SetActive(false);
            _inventoryVisible = false;
            _pauseManager.ResumeGame();
        }
        else
        {
            _inventoryPanel.SetActive(true);
            _inventoryVisible = true;
            _pauseManager.PauseGame();
            DisplayInventory();
        }
    }

    public void DisplayInventory()
    {
        int index = 0;
       foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
       {
            _slots[index].sprite = entry.Key.image;
            _quantityText = _slots[index].GetComponentInChildren<TMP_Text>();
            _quantityText.text = entry.Value.ToString();
            index++;
       }
    }
}
