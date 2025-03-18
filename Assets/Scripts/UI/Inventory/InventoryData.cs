using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public List<InventorySlotData> slots = new List<InventorySlotData>();
}

[System.Serializable]
public class InventorySlotData
{
    public string itemName;  // Use unique identifiers for items
    public int quantity;
    public int slotIndex;  // Position in the inventory UI
}
