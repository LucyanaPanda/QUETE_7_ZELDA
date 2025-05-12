using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance;

    [Header("Inventory")]
    public  Dictionary<Item, int> inventory = new Dictionary<Item, int>();
    public List<Item> items = new List<Item>();
    public InventoryUI inventoryUi;

    [Header("Money")]
    public int money;

    [Header("SaveInventory")]
    public SaveInventory saveInventory;

    private void Awake()
    {
        if (Instance != null) { Destroy(this); }
        else { Instance = this; }
    }

    private void Start()
    {
        saveInventory = SaveInventory.Instance;
        money = saveInventory.LoadMoney();
    }
    public void AddMoney(int value)
    {
        money += value;
        saveInventory.SaveMoney();
    }

    public bool AddToInventory(ItemScript item)
    {
        if (ItemInInventory(item))
        {
            inventory[item.ItemData]++;
            inventoryUi.UpdateInventory(item);
        } else
        {
            if (inventory.Count < inventoryUi.slots.Count )
                inventory.Add(item.ItemData, 1);
            else
            {
                Debug.Log("Inventory Full");
                return false;
            }
        }
        saveInventory.SaveTheInventory();
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
}

