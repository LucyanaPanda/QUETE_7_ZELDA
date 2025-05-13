using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private PlayerInventory _playerInventory;
    [SerializeField] private GameObject _inventoryPanel;
    [SerializeField] private TMP_Text _moneyText;
    public List<Slot> slots;
    public bool inventoryVisible = false;

    [Header("Player Interface")]
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Image _image;
    [SerializeField] private GameObject _playerHotbar;
    [SerializeField] private GameObject _playerHpBar;

    [Header("PauseManager")]
    [SerializeField] private PauseManager _pauseManager;

    [Header("SaveInventory")]
    private SaveInventory _saveInventory;

    private void Start()
    {
        _playerInventory = PlayerInventory.Instance;
        _saveInventory = SaveInventory.Instance;

        _image.sprite = _playerManager.creatureData.image;
        InitializeSlotsPositions();
        _inventoryPanel.SetActive(true);
        _playerHpBar.SetActive(false);
        _playerHpBar.SetActive(false);
        inventoryVisible = true;
        _pauseManager.PauseGame();

        LoadAndDisplayInventory();

        _inventoryPanel.SetActive(false);
        _playerHpBar.SetActive(true);
        _playerHpBar.SetActive(true);
        inventoryVisible = false;
        _saveInventory.SaveTheInventory();
        _pauseManager.ResumeGame();
    }

    public void ShowHideInventory(InputAction.CallbackContext context)
    {
        if (inventoryVisible)
        {
            _inventoryPanel.SetActive(false);
            _playerHpBar.SetActive(true);
            _playerHpBar.SetActive(true);
            inventoryVisible = false;
            _saveInventory.SaveTheInventory();
            _pauseManager.ResumeGame();
            GameManager.Instance.playerInGame = true;
        }
        else
        {
            GameManager.Instance.playerInGame = false;
            _inventoryPanel.SetActive(true);
            _playerHpBar.SetActive(false);
            _playerHpBar.SetActive(false);
            inventoryVisible = true;
            _pauseManager.PauseGame();
            _saveInventory.LoadInventory();
            LoadAndDisplayInventory();
            _moneyText.text = "Money:" + _playerInventory.money;
        }
    }

    public void InitializeSlotsPositions()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].position = i;
        }
    }

    public void LoadAndDisplayInventory()
    {
        _saveInventory.LoadInventory();
        List<int> occupiedSlots = new List<int>();

        foreach (KeyValuePair<Item, int> entry in _playerInventory.inventory)
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

        if (_playerInventory.inventory.Count <= 0) { return; }

        for(int i = 0; i < slots.Count; i++)
        {
            if (!occupiedSlots.Contains(i))
            {
                slots[i].image.sprite = null;
                slots[i].quantityText.text = "";
                slots[i].dragableItem.currentItem = null;
            }
        }
    }

    private int GetSavedSlotIndex(Item item)
    {
        string json = PlayerPrefs.GetString(_saveInventory.inventorySaveKey);
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

    public void UpdateInventory(ItemScript item)
    {
        foreach( Slot slot in slots)
        {
            if (slot.dragableItem != null && slot.dragableItem.currentItem != null && slot.dragableItem.currentItem == item.ItemData)
            {
                slot.UpdateInformation();
            }
        }
    }

}
