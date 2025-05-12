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
        _boxCollider.enabled = false;
    }

    public virtual void Attack() 
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable iDamageable = collision.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            iDamageable.TakeDamage(damage);
        }
    }
}
