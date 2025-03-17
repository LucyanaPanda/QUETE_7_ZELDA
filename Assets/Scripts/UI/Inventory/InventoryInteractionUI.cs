using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryInteractionUI : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private List<InventorySlot> _slots;
    [SerializeField] private Sprite _emptySlotSprite;
}
