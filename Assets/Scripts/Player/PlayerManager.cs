using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [Header("Data")]
    public Creature creatureData;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private float _health, _minHealth, _maxHealth;
    private float _defense, _minDefense, _maxDefense;

    private void Start()
    {
        //Sprite
        _spriteRenderer.sprite = creatureData.image;

        //Health
        _health = creatureData.health;
        _minHealth = creatureData.minHealth;
        _maxHealth = creatureData.maxHealth;

        //Defense
        _defense = creatureData.defense;
        _minDefense = creatureData.minDefense;
        _maxDefense = creatureData.maxDefense;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(Damage(damage));
    }

    public IEnumerator Damage(float damage)
    {
        Debug.Log(damage - _defense);
        _health -= damage - _defense;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        _spriteRenderer.color = Color.white;
        IfDead();
    }

    private void IfDead()
    {
        if (_health < _minHealth)
            Destroy(gameObject);
    }
}
