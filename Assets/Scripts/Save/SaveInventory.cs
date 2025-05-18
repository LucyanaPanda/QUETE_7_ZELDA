using System.Collections.Generic;
using UnityEngine;

public class SaveInventory : MonoBehaviour
{
    public static SaveInventory Instance;
    public PlayerInventory playerInventory;
    public InventoryUI inventoryUi;

    [Header("Money")]
    public readonly string moneySaveKey = "player_money";

    [Header("Inventory")]
    public readonly string inventorySaveKey = "player_inventory";

    private void Awake()
    {
        if (Instance != null) { Destroy(this); }
        else { Instance = this; }
        inventoryUi = playerInventory.inventoryUi;
    }

    private void Start()
    {
        inventoryUi = playerInventory.inventoryUi;
    }

    #region Save Money
    public void SaveMoney()
    {
        PlayerPrefs.SetInt(moneySaveKey, playerInventory.money);
    }

    public int LoadMoney()
    {
        if (!PlayerPrefs.HasKey(moneySaveKey)) return 0;
        playerInventory.money = PlayerPrefs.GetInt(moneySaveKey);
        return playerInventory.money;
    }
    #endregion

    public void SaveTheInventory()
    {
        InventoryData inventoryData = new InventoryData();

        foreach (KeyValuePair<Item, int> entry in playerInventory.inventory)
        {
            SlotData slotData = new SlotData
            {
                itemName = entry.Key.name,
                quantity = entry.Value,
                slotIndex = GetItemSlot(entry.Key)
            };

            inventoryData.slots.Add(slotData);
        }

        string json = JsonUtility.ToJson(inventoryData);
        playerInventory.DisplayInventory();
        PlayerPrefs.SetString(inventorySaveKey, json);
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(inventorySaveKey))
        {
            string json = PlayerPrefs.GetString(inventorySaveKey);
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);

            playerInventory.inventory.Clear();
            if (inventoryData == null)
            {
                SaveTheInventory();
                return;
            }

            foreach (SlotData slotData in inventoryData.slots)
            {
                Item item = FindItemByName(slotData.itemName);
                if (item != null)
                    playerInventory.inventory[item] = slotData.quantity;
            }
        }
    }

    public Item FindItemByName(string name)
    {
        for (int i = 0; i < playerInventory.items.Count; i++)
        {
            if (playerInventory.items[i].name == name)
            {
                return playerInventory.items[i];
            }
        }
        return null;
    }

    public int GetItemSlot(Item item)
    {
        foreach (Slot slot in inventoryUi.slots)
        {
            if (slot.dragableItem != null && slot.dragableItem.currentItem == item)
            {
                return slot.position;
            }
        }
        return -1;
    }
}
