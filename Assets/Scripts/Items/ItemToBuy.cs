using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemToBuy : MonoBehaviour
{
    public Product product;
    public PlayerInventory _playerInventory;
    public bool _isSoldOut;

    [Header("Ui Elements")]
    [SerializeField] private TMP_Text _quantityText;
    [SerializeField] private TMP_Text _nameItemText;
    [SerializeField] private TMP_Text _descriptionItemText;
    [SerializeField] private TMP_Text _priceText;
    [SerializeField] private Image _itemImage;
    [SerializeField] private GameObject _soldOutNotice;
    [SerializeField] private Button _buyButton;

    public NPCShop shop;

    private void Start()    
    {
        _playerInventory = PlayerInventory.Instance;

        Item itemData = product.item.ItemData;
        _quantityText.text = "products avilable:" + product.quantity;
        _nameItemText.text = itemData.nameItem;
        _descriptionItemText.text = itemData.description;
        _priceText.text = itemData.price.ToString();
        _itemImage.sprite = itemData.image;

        _buyButton.onClick.AddListener(() => BuyItem(product.item));

        if (product.quantity <= 0)
        {
            SoldOut();
        }
    }

    public bool BuyItem(ItemScript item)
    {
        if (item.ItemData.price > _playerInventory.money)
        {
            Debug.Log("Cannot buy this item");
            return false;
        }
        _playerInventory.AddMoney(-item.ItemData.price);
        _playerInventory.saveInventory.SaveMoney();
        _playerInventory.AddToInventory(item, 1);
        product.quantity--;
        UpdateQuantity();
        shop.DisplayMoney();
        return true;
    }

    private void SoldOut()
    {
        Debug.Log("It's sold out");
        _buyButton.onClick.RemoveAllListeners();
        Image image = _buyButton.GetComponent<Image>();
        image.color = Color.red;
        _soldOutNotice.gameObject.SetActive(true);
    }

    private void UpdateQuantity()
    {
        _quantityText.text = "products avilable:" + product.quantity;
        shop.ReduceQuantity(product, product.quantity);
        if (product.quantity <= 0) { SoldOut(); return; }
    }

    
}
