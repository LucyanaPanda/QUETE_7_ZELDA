using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour, IDamageable
{
    [Header("Health")]
    [SerializeField] private float _health;
    private SpriteRenderer _spriteRenderer;

    public float health => _health;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(Damage(damage));
    }

    public IEnumerator Damage(float damage)
    {
        _health -= damage;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        _spriteRenderer.color = Color.white;
        IfDead();
    } 

    private void IfDead()
    {
        if (_health <= 0)
            Destroy(gameObject);
    }

}
