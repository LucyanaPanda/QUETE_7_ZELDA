using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableScript
{
    [SerializeField] private List<GameObject> _items;
    [SerializeField] private float _rad;
    [SerializeField] private bool _isInFront;
    [SerializeField] private GameObject _chest;

    
    public string name => "Chest";

    public override void Interact()
    {
        _audioSource.clip = _audioclip;
        _audioSource.Play();
        foreach (GameObject item in _items)
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
        Destroy(_chest);
    }
}
