using System.Collections.Generic;
using UnityEngine;

public class EntryEnigma : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private Item _questObject;
    [SerializeField] private GameObject _door;
    [SerializeField] private GameObject _entry;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inventory = collision.GetComponent<PlayerInventory>();
    }

    public void Interact()
    {
        foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
        {
            if (entry.Key == _questObject)
            {
                PlayerInventory.inventory.Remove(entry.Key);
                _inventory.DisplayInventory();
                _inventory.SaveInventory();
                _inventory.LoadInventory();
                _door.SetActive(false);
                _entry.SetActive(true);
                Destroy(this);
            }
        }
    }
}
