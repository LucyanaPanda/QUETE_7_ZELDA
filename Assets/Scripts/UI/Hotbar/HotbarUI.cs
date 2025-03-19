using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarUI : MonoBehaviour
{
    [Header("Hotbar")]
    [SerializeField] private PlayerHotbar _playerHotbar;
    [SerializeField] private GameObject _horbarPanelInventory;
    [SerializeField] private GameObject _horbarPanelInGame;
    public List<Slot> slots;
    public bool hotbarInventoryVisible = false;

    [Header("PauseManager")]
    [SerializeField] private PauseManager _pauseManager;

    private void Start()
    {
        InitializeSlotsPositions();
        LoadAndDisplayHotbar();
    }

    public void InitializeSlotsPositions()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].position = i;
        }
    }

    public void LoadAndDisplayHotbar()
    {
        List<int> occupiedSlots = new List<int>();

        foreach (KeyValuePair<Item, int> entry in PlayerHotbar.hotbar)
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
        string json = PlayerPrefs.GetString(PlayerHotbar.hotbarSaveKey);
        HotbarData hotbarData = JsonUtility.FromJson<HotbarData>(json);

        if (hotbarData != null)
        {
            foreach (SlotData slotData in hotbarData.slots)
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
