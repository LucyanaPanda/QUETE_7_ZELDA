using System.Collections.Generic;

[System.Serializable]
public class InventoryData
{
    public List<SlotData> slots = new List<SlotData>();
}

[System.Serializable]
public class SlotData
{
    public string itemName;  // Use unique identifiers for items
    public int quantity;
    public int slotIndex;  // Position in the inventory UI
}
