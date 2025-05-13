using UnityEngine;

public class PoisonedArrows : Arrow
{
    IDamageable target;

    [Header("Timer")]
    [SerializeField] private float maxTimer;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > maxTimer)
        {
            PoisonTarget();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable iDamageable = collision.GetComponent<IDamageable>();
        if (iDamageable != null && tagParent != "" && collision.tag != tagParent)
        {
            iDamageable.TakeDamage(damage);
            target = iDamageable;
        }
    }

    public void PoisonTarget()
    {
        if (target != null)
        {
            target.TakeDamage(damage);
        }
    }
}
