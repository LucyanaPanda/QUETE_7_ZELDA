using UnityEngine;

public interface IDamageable
{
    public float health { get; }

    public void TakeDamage(float damage) { }
}