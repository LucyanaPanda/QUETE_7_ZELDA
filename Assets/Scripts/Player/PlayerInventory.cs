using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public static Dictionary<string, int> _inventory = new Dictionary<string, int>();

    [Header("Money")]
    public static int money;

    public void AddToInventory(ItemScript item)
    {
        if (ItemInInventory(item))
        {
            _inventory[item.ItemData.name]++;
        } else 
            _inventory.Add(item.ItemData.name, 1);
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
            if (_inventory[item.ItemData.name] > 0)
            {
                _inventory[item.ItemData.name]--;
                if (_inventory[item.ItemData.name] == 0)
                {
                    _inventory.Remove(item.ItemData.name);
                }
            }
        }
    }

    public bool ItemInInventory(ItemScript item)
    {
        return _inventory.ContainsKey(item.ItemData.name);
    }

    public void DisplayInventory()
    {
        foreach (KeyValuePair<string, int> entry in _inventory)
        {
            Debug.Log(entry.Key + " : " + entry.Value);
        }
    }
}
