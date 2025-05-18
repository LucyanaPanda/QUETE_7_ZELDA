using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    [Header("Special objects to unlock lore")]
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private ItemScript _orb;
    [SerializeField] private ItemScript _concertTicket;

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

    public bool AddToInventory(ItemScript item, int quantity)
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
        CheckIfLoreObject();
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

    public void CheckIfLoreObject()
    {
        if (ItemInInventory(_orb))
        {
            _gameManager.StartLore(_gameManager.loreOrb, _gameManager.keyLoreOrb, false);
        }
        
        if (ItemInInventory(_concertTicket))
        {
            _gameManager.StartLore(_gameManager.loreEnding, _gameManager.keyLoreEnding, true);
        }
        return;
    }
}

