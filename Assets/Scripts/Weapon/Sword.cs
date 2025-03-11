using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Transform))]
public class Sword : MonoBehaviour
{
    private float _damage = 1.5f;
    private Animator _animator;

    void OnEnable()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(LaunchAnimationAttack());
    }

    IEnumerator LaunchAnimationAttack()
    {
        _animator.SetTrigger("Attack");
        yield return new WaitForSecondsRealtime(0.4f);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable iDamageable = collision.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            iDamageable.TakeDamage(_damage);
        }
    }
}
