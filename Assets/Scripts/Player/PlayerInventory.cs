using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public static Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    public List<Item> _items = new List<Item>();
    public static readonly string inventorySaveKey = "player_inventory";
    public InventoryUI _inventoryUi;

    [Header("Money")]
    public static int money;

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
        return true;
    }

    //public void BuyItem(ItemScript item)
    //{
    //    if (item.ItemData.price < PlayerInventory.money)
    //    {
    //        Debug.Log("Cannot buy this item");
    //        return;
    //    }
    //    PlayerInventory.money -= item.ItemData.price;
    //    AddToInventory(item);
    //    //Add a sound clip
    //}

    //public static void RemoveAnItem(ItemScript item)
    //{
    //    if (ItemInInventory(item))
    //    {
    //        if (inventory[item.ItemData] > 0)
    //        {
    //            inventory[item.ItemData]--;
    //            if (inventory[item.ItemData] == 0)
    //            {
    //                inventory.Remove(item.ItemData);
    //            }
    //        }
    //    }
    //}

    public static bool ItemInInventory(ItemScript item)
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
            SlotData slotData = new SlotData
            {
                itemName = entry.Key.name, // Assuming item name is unique
                quantity = entry.Value,
                slotIndex = GetItemSlot(entry.Key)  // Get the UI position of the item
            };

            inventoryData.slots.Add(slotData);
        }

        string json = JsonUtility.ToJson(inventoryData);
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

            foreach (SlotData slotData in inventoryData.slots)
            {
                Item item = FindItemByName(slotData.itemName);
                if (item != null)
                {
                    inventory[item] = slotData.quantity;
                }
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
}
