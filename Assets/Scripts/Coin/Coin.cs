using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _amount;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerInventory player = collision.gameObject.GetComponent<PlayerInventory>();
        if (player != null)
        {
            player.AddMoney(_amount);
            Destroy(this.gameObject);
        }
    }
}
