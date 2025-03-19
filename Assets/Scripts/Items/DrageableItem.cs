using System.Resources;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class DrageableItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Drag and drop")]
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _transform;
    public Transform _transformParent;
    public Slot _currentSlot;

    [Header("Item Infos")]
    public Item currentItem;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _transformParent =  _transform.parent;
        _transform.SetParent(_transform.root);
        _transform.SetAsLastSibling();
        _image.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        OnEndDragging();
    }

    public void OnEndDragging()
    {
        _transform.SetParent(_transformParent);
        _transform.localPosition = Vector2.zero;
        _image.raycastTarget = true;
    }
}
