using UnityEngine;

public class Sword : Weapon
{

    public override void Attack()
    {
        _spriteRenderer.enabled = true;
        _boxCollider.enabled = true;
        _animator.SetTrigger("Attack");

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
