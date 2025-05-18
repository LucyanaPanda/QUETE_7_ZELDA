using System.Collections;
using UnityEngine;

public class Sword : Weapon
{
    public override void Attack()
    {
        
        _animator.SetTrigger("Attack");
        StartCoroutine(DeactiveComponents());

    }

    IEnumerator DeactiveComponents()
    {
        yield return new WaitForSecondsRealtime(0.4f);
        _spriteRenderer.enabled = false;
        _boxCollider.enabled = false;
        Debug.Log("Dactiv");
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
