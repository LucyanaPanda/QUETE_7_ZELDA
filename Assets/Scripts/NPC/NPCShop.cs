using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCShop : MonoBehaviour
{
    [Header("Shop Elements")]
    [SerializeField] private GameObject _shopPanel;
    [SerializeField] private Button _quitShopButton;
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private GameObject _itemToBuyPrefab;
    [SerializeField] private GameObject _content;

    [Header("Products to sale")]
    [SerializeField] private Product[] products;
    [SerializeField] private List<GameObject> items = new List<GameObject>();

    private void OnEnable()
    {
        ShowHideShop(true);
        DispayProducts();
        DisplayMoney();
        _quitShopButton.onClick.AddListener(() => ShowHideShop(false));
        _quitShopButton.onClick.AddListener(() => ClearShop());
    }

    private void OnDisable()
    {
        ClearShop();
        _quitShopButton.onClick.RemoveAllListeners();
    }

    private void DispayProducts()
    {
        items.Clear();
        foreach(Product product in products)
        {
            GameObject item = Instantiate(_itemToBuyPrefab, _content.transform, false);
            ItemToBuy itemToBuy = item.GetComponent<ItemToBuy>();
            itemToBuy.product = product;
            itemToBuy.shop = this;
            items.Add(itemToBuy.gameObject);
        }
    }

    private void ClearShop()
    {
        foreach (GameObject item in items)
        {
            Destroy(item);
        }
    }

    private void DisplayMoney()
    {
        _moneyText.text = PlayerInventory.Instance.money.ToString();
    }

    public void ShowHideShop(bool show)
    {
        _shopPanel.SetActive(show);
    }

    public void ReduceQuantity(Product productSearched, int newQuantity)
    {
        for (int i = 0; i < products.Length; i++)
        {
            if (products[i].item == productSearched.item)
            {
                products[i].quantity = newQuantity;
            }
        }
    }
}

[System.Serializable]
public struct Product
{
    public ItemScript item;
    public int quantity;
}
