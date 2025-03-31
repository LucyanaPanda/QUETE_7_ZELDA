using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    [Header("Identification")]
    public int id;
    public string nameItem;
    public string description;
    public Sprite image;
    public bool canBeUse;
    public bool isPotion;
    public bool equipment;

    [Header("Price")]
    public int price;

    [Header("Boosts")]
    public float healthBoost;
    public float speedBoost;
    public float defBoost;
    public float attackBoost;

    [Header("Malus")]
    public float healthMalus;
    public float speedMalus;
    public float defMalus;
    public float attackMalus;

    [Header("Duration")]
    public float duration;
    public bool hasDuration;

}
