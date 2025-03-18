using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public static Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    [SerializeField] private List<Item> _items = new List<Item>();
    private const string INVENTORY_SAVE_KEY = "player_inventory";

    [Header("Money")]
    public static int money;

    public void AddToInventory(ItemScript item)
    {
        if (ItemInInventory(item))
        {
            inventory[item.ItemData]++;
        } else 
            inventory.Add(item.ItemData, 1);
        DisplayInventory();
    }

    public void BuyItem(ItemScript item)
    {
        if (item.ItemData.price < PlayerInventory.money)
        {
            Debug.Log("Cannot buy this item");
            return;
        }
        PlayerInventory.money -= item.ItemData.price;
        AddToInventory(item);
    }

    public void RemoveAnItem(ItemScript item)
    {
        if (ItemInInventory(item))
        {
            if (inventory[item.ItemData] > 0)
            {
                inventory[item.ItemData]--;
                if (inventory[item.ItemData] == 0)
                {
                    inventory.Remove(item.ItemData);
                }
            }
        }
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

    public void SaveInventory()
    {
        InventoryData inventoryData = new InventoryData();

        foreach (KeyValuePair<Item, int> entry in inventory)
        {
            InventorySlotData slotData = new InventorySlotData
            {
                itemName = entry.Key.name, // Assuming item name is unique
                quantity = entry.Value,
                slotIndex = GetItemSlot(entry.Key)  // Get the UI position of the item
            };

            inventoryData.slots.Add(slotData);
        }

        string json = JsonUtility.ToJson(inventoryData);
        PlayerPrefs.SetString(INVENTORY_SAVE_KEY, json);
        PlayerPrefs.Save();
    }

    public void LoadInventory()
    {
        if (PlayerPrefs.HasKey(INVENTORY_SAVE_KEY))
        {
            string json = PlayerPrefs.GetString(INVENTORY_SAVE_KEY);
            InventoryData inventoryData = JsonUtility.FromJson<InventoryData>(json);

            inventory.Clear();

            foreach (InventorySlotData slotData in inventoryData.slots)
            {
                Item item = FindItemByName(slotData.itemName);
                if (item != null)
                {
                    inventory[item] = slotData.quantity;
                }
            }
        }
    }

    private Item FindItemByName(string name)
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

    private int GetItemSlot(Item item)
    {
        // Implement a method to retrieve the current UI slot position of an item
        return 0;
    }
}
