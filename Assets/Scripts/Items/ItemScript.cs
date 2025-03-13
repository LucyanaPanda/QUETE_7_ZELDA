using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class ItemScript : MonoBehaviour
{
    public Item ItemData;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.sprite = ItemData.image;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == null)
        {
            Debug.Log("Return");
            return;
        }
        Debug.Log("Triggered");
        //PlayerInventory inventory = collision.GetComponent<PlayerInventory>();
        //if (inventory != null)
        //{
        //    inventory.AddToInventory(this);
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    Debug.Log("ojhsf");
        //}
    }
}
