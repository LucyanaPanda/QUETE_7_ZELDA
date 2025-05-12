using System.Collections.Generic;
using UnityEngine;

public class EntryEnigma : MonoBehaviour, IInteractable
{
    private PlayerInventory _inventory;
    [SerializeField] private Item _questObject;
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _entry;

    private void Start()
    {
        _inventory = PlayerInventory.Instance;
    }

    public void Interact()
    {
        foreach (KeyValuePair<Item, int> entry in _inventory.inventory)
        {
            if (entry.Key == _questObject)
            {
                _inventory.inventory.Remove(entry.Key);
                _inventory.saveInventory.SaveTheInventory();
                _inventory.saveInventory.LoadInventory();
                _door.SetActive(false);
                _entry.SetActive(true);
                Destroy(this);
                break;
            }
        }
    }
}
