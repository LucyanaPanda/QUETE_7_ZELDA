using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [Header("Inventory")]
    public static Dictionary<Item, int> inventory = new Dictionary<Item, int>();

    [Header("Money")]
    public static int money;

    public void AddMoney(int amout)
    {
        money += amout;
        Debug.Log("Current money:" + money.ToString());
        //PlayerManager.PlayCoinSound(_coinClip, _audioSource);
    }

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
}
