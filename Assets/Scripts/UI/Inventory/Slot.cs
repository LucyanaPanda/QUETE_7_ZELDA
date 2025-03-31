using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IDropHandler
{
    [Header("Slot")]
    public Image image;
    public TMP_Text quantityText;
    public int position;
    public bool _isSlotHotbar;
    public bool _isSlotWeapon;
    public bool _isSlotArmor;
    public bool _isSlotAccesorie;

    [Header("Item")]
    public DrageableItem dragableItem;

    [Header("PlayerEquipment")]
    [SerializeField] private PlayerEquipment _playerEquipment;


    private void Awake()
    {
        dragableItem = GetComponentInChildren<DrageableItem>();
        GetNecessaryComponents();
    }


    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        DrageableItem droppedItem = eventData.pointerDrag.GetComponent<DrageableItem>(); // Get the item dropped
        if (_isSlotWeapon  && !droppedItem.currentItem.isWeapon)
        {
            return;
        }
        if (_isSlotArmor && !droppedItem.currentItem.isArmor)
        {
            return;
        }
        if (_isSlotAccesorie && !droppedItem.currentItem.isAccesorie)
        {
            return;
        }

        SwipeItem(droppedItem);

        if (_isSlotHotbar)
        {
            MoveItems();
        }
        if (_isSlotWeapon || _isSlotArmor ||_isSlotAccesorie)
        {
            _playerEquipment.Equipement();
        }
    }

    public void SwipeItem(DrageableItem item)
    {
        Slot slot = item._transformParent.gameObject.GetComponent<Slot>(); // Get the slot of the item dropped
        DrageableItem copyCurrentItem = dragableItem; // make a copy of the current item of this slot
        Transform transformParentOtherItem = item._transformParent; // make a copy of the transform's item dropped
        item._transformParent = transform; // Item dropped became infant of this slot
        dragableItem._transformParent = transformParentOtherItem; // the "current" item of this slot became the infant of the other slot
        dragableItem.OnEndDragging(); // Reset their pos;
        dragableItem = item; // drageable item of this slot is the item dropped
        slot.dragableItem = copyCurrentItem; // dragable item of the other slot is the swapped item;

        slot.GetNecessaryComponents();
        GetNecessaryComponents();

        if ((_isSlotWeapon || _isSlotArmor || _isSlotAccesorie) || (slot._isSlotWeapon || slot._isSlotArmor || slot._isSlotAccesorie))
            _playerEquipment.Equipement();
    }

    public void MoveItems()
    {
        foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
        {
            if (entry.Key == dragableItem.currentItem)
            {
                PlayerHotbar.AddItem(entry.Key, entry.Value);
            }
        }
    }

    public void GetNecessaryComponents()
    {
        image = dragableItem.GetComponent<Image>();
        quantityText = dragableItem.GetComponentInChildren<TMP_Text>();
    }

    public void UpdateInformation()
    {
        if (dragableItem.currentItem != null)
        {
            if (PlayerInventory.inventory.ContainsKey(dragableItem.currentItem))
            {
                quantityText.text = PlayerInventory.inventory[dragableItem.currentItem].ToString();
                return;
            }

            quantityText.text = PlayerHotbar.hotbar[dragableItem.currentItem].ToString();
            return;
            
        }

        image.sprite = null;
        quantityText.text = "";
    }
}
