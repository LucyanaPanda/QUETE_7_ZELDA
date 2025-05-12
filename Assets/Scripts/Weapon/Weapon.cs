using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    public float damage;
    protected Animator _animator;

    [Header("Components")]
    protected SpriteRenderer _spriteRenderer;
    protected BoxCollider2D _boxCollider;
    
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.enabled = false;

        _boxCollider = GetComponent<BoxCollider2D>();
        try
        {
            _boxCollider.enabled = false;
        }
        catch (Exception e) { }
    }

    public virtual void Attack() 
    {

    }
}
