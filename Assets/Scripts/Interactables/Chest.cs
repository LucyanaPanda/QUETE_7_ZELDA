using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] private List<GameObject> _items;
    public string name => "Chest";

   public void Interact() 
    { 

    }

    private void DropItem()
    {
        foreach (GameObject item in _items)
        {
            int quantity;
            if (_items.Count > 1)
                quantity = Random.Range(0, _items.Count);
            else
                quantity = Random.Range(0, _items.Count + 2);
            Debug.Log("Quantity:" + quantity);
            for (int i = 0; i < quantity; i++)
            {
                float x = Random.Range(0f, 2f);
                float y = Random.Range(0f, 2f);

                Vector2 position = new Vector2(x + transform.position.x, y + transform.position.y);
                Instantiate(item, position, Quaternion.identity);
            }
        }
    }
}
