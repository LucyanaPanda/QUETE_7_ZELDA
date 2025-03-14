using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Slot : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [Header("Slot")]
    public Image image;
    public TMP_Text quantityText;
    public int position;

    [Header("Item")]
    public Item currentItem;

    [Header("DragAndDrop")]
    [SerializeField] private bool _isDragging;
    [SerializeField] private float _timeBeforeDraggring;
    [SerializeField] private float _timeForDragging;

    private void Awake()
    {
        image = GetComponent<Image>();
        quantityText = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        if (_isDragging)
        { 
            _timeBeforeDraggring += Time.deltaTime; 
            if (_timeBeforeDraggring >= _timeForDragging )
            {

            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
        //Check if the mouse is hovering a slot
        //if it is, then change the positione of the item holding to the one chosen
        //if there was a item on that chosen slot, swap;
    }
}
