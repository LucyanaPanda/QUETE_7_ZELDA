using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [Header("Slot")]
    public Image image;
    public TMP_Text quantityText;
    public int position;

    [Header("Item")]
    public DrageableItem dragableItem;

    private void Awake()
    {
        dragableItem = GetComponentInChildren<DrageableItem>();
        GetNecessaryComponents();
    }


    public void OnDrop(PointerEventData eventData)
    {
        
        SwipeItem(eventData.pointerDrag);
    }

    public void SwipeItem(GameObject droppedItem)
    {

        DrageableItem item = droppedItem.GetComponent<DrageableItem>(); // Get the item dropped
        InventorySlot slot = item._transformParent.gameObject.GetComponent<InventorySlot>(); // Get the slot of the item dropped
        DrageableItem copyCurrentItem = dragableItem; // make a copy of the current item of this slot
        Transform transformParentOtherItem = item._transformParent; // make a copy of the transform's item dropped
        item._transformParent = transform; // Item dropped became infant of this slot
        dragableItem._transformParent = transformParentOtherItem; // the "current" item of this slot became the infant of the other slot
        dragableItem.OnEndDragging(); // Reset their pos;
        dragableItem = item; // drageable item of this slot is the item dropped
        slot.dragableItem = copyCurrentItem; // dragable item of the other slot is the swapped item;

        slot.GetNecessaryComponents();
        GetNecessaryComponents();
    }

    public void GetNecessaryComponents()
    {
        image = dragableItem.GetComponent<Image>();
        quantityText = dragableItem.GetComponentInChildren<TMP_Text>();
    }
}
