using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //Inventory
    public int[,] _inventory;
    public int _money;

    public void AddToInventory(ItemScript item)
    {
        for (int i = 0; i < _inventory.GetLength(0); i++)
        {
            if (_inventory[0, i] == item.ItemData.id)
            {
                _inventory[1, i]++;
                Debug.Log("Successfully");
            }
        }
    }
}
