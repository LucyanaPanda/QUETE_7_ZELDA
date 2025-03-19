using System.Collections.Generic;
using UnityEngine;

public class PlayerHotbar : MonoBehaviour
{
    [Header("Hotbar")]
    public static Dictionary<Item, int> hotbar = new Dictionary<Item, int>();
    public List<Item> _items = new List<Item>();
    public static readonly string hotbarSaveKey = "player_hotbar";
    [SerializeField] private HotbarUI _hotbarUi;

    private void Start()
    {
        LoadHotbar();
    }

    public void SaveHotbar()
    {
        HotbarData hotbarData = new HotbarData();

        foreach (KeyValuePair<Item, int> entry in hotbar)
        {
            SlotData slotData = new SlotData
            {
                itemName = entry.Key.name, // Assuming item name is unique
                quantity = entry.Value,
                slotIndex = GetItemSlot(entry.Key)  // Get the UI position of the item
            };

            hotbarData.slots.Add(slotData);
        }

        string json = JsonUtility.ToJson(hotbarData);
        PlayerPrefs.SetString(hotbarSaveKey, json);
        PlayerPrefs.Save();
    }

    public void LoadHotbar()
    {
        if (PlayerPrefs.HasKey(hotbarSaveKey))
        {
            string json = PlayerPrefs.GetString(hotbarSaveKey);
            HotbarData hotbarData = JsonUtility.FromJson<HotbarData>(json);

            hotbar.Clear();

            foreach (SlotData slotData in hotbarData.slots)
            {
                Item item = FindItemByName(slotData.itemName);
                if (item != null)
                {
                    hotbar[item] = slotData.quantity;
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
        foreach (Slot slot in _hotbarUi.slots)
        {
            if (slot.dragableItem != null && slot.dragableItem.currentItem == item)
            {
                return slot.position;
            }
        }
        return -1;
    }
}
