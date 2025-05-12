
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Weapon : MonoBehaviour
{
    public float damage;
    protected Animator _animator;

    [Header("Components")]
    protected SpriteRenderer _spriteRenderer;
    protected BoxCollider2D _boxCollider;

    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        _spriteRenderer.enabled = false;
        if (_boxCollider != null )
        {
            _boxCollider.enabled = false;
        }
    }

    public virtual void Attack() 
    {

    }
}
