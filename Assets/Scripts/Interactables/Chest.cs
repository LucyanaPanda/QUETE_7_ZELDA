using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableScript, IInteractable
{
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private float _rad;
    [SerializeField] private bool _isInFront;
    [SerializeField] private GameObject _chest;
    public string name => "Chest";

    public void Interact()
    {
        Debug.Log("Open chest");
        foreach (GameObject item in _items)
        {
            int quantity;
            if (_items.Count > 1)
                quantity = Random.Range(1, _items.Count);
            else
                quantity = Random.Range(1, _items.Count + 2);
            Debug.Log("Quantity:" + quantity);
            for (int i = 0; i < quantity; i++)
            {
                float x = Random.Range(-_rad, _rad);
                float y;
                if (_isInFront)
                    y = Random.Range(-2, 0f);
                else
                    y = Random.Range(0f, 2f);

                Vector2 position = new Vector2(x + transform.position.x, y + transform.position.y);
                Instantiate(item, position, Quaternion.identity);
            }
        }

        Destroy(_chest);
    }
}
