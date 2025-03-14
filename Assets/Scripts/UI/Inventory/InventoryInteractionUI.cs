using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class InventoryInteractionUI : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private List<Slot> _slots;
    [SerializeField] private Sprite _emptySlotSprite;
}
