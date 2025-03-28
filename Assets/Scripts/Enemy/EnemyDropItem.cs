using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropItem : MonoBehaviour
{
    [Header("Item")]
    [SerializeField] private List<GameObject> _items;

    [Header("EnemyManager")]
    [SerializeField] private EnemyManager _enemyManager;

    private void Start()
    {
        _enemyManager.OnDeath.AddListener(() => DropItem());
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
