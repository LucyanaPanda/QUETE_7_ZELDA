using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("Inventory")]
    public static readonly string inventorySaveKey = "player_inventory";
    public static Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    public List<Item> _items = new List<Item>();
    public InventoryUI _inventoryUi;

    [Header("Money")]
    public static readonly string moneySaveKey = "player_money";
    public static int money;

    private void Awake()
    {
        if (Instance != null) { Destroy(this); }
        else { Instance = this; }
    }

    private void Start()
    {
        money = LoadMoney();
    }

    public bool AddToInventory(ItemScript item)
    {
        if (ItemInInventory(item))
        {
            inventory[item.ItemData]++;
            _inventoryUi.UpdateInventory(item);
        } else
        {
            if (inventory.Count < _inventoryUi.slots.Count )
                inventory.Add(item.ItemData, 1);
            else
            {
                Debug.Log("Inventory Full");
                return false;
            }
        }
        SaveInventory();
        DisplayInventory();
        return true;
    }

    public bool ItemInInventory(ItemScript item)
    {
        return inventory.ContainsKey(item.ItemData);
    }

    public void DisplayInventory()
    {
        foreach (KeyValuePair<Item, int> entry in inventory)
        {
            Debug.Log(entry.Key + " : " + entry.Value);
        }
    }

    #region Save Money
    public void AddMoney(int value)
    {
        money += value;
        SaveMoney();
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt(moneySaveKey, PlayerInventory.money);
    }

    public int LoadMoney()
    {
        if (!PlayerPrefs.HasKey(moneySaveKey)) return 0;
        money = PlayerPrefs.GetInt(moneySaveKey);
        return money;
    }
    #endregion

    #region Save Inventory
    public void SaveInventory()
    {
        InventoryData inventoryData = new InventoryData();

        foreach (KeyValuePair<Item, int> entry in inventory)
        {
            SlotData slotData = new SlotData
            {
                itemName = entry.Key.name, // Assuming item name is unique
                quantity = entry.Value,
                slotIndex = GetItemSlot(entry.Key)  // Get the UI position of the item
            };

            inventoryData.slots.Add(slotData);
        }

        string json = JsonUtility.ToJson(inventoryData);
        DisplayInventory();
        PlayerPrefs.SetString(inventorySaveKey, json);
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(inventorySaveKey))
        {
            string json = PlayerPrefs.GetString(inventorySaveKey);
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);

            inventory.Clear();
            if (inventoryData == null)
            {
                 SaveInventory();
                 return;
            }

            foreach (SlotData slotData in inventoryData.slots)
            {
                Item item = FindItemByName(slotData.itemName);
                if (item != null)
                    inventory[item] = slotData.quantity;
            }
        }
    }

    public Item FindItemByName(string name)
    {
        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].name == name)
            {
                return _items[i];
            }
        }
        return null;
    }

    public int GetItemSlot(Item item)
    {
        foreach (Slot slot in _inventoryUi.slots)
        {
            if (slot.dragableItem != null && slot.dragableItem.currentItem == item)
            {
                return slot.position;
            }
        }
        return -1;
    }
    #endregion
}

