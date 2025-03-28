using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class EnemyManager : MonoBehaviour, IDamageable
{
    [Header("Data")]
    public Creature creatureData;
    public float health, minHealth, maxHealth;
    public float defense, minDefense, maxDefense;
    public SpriteRenderer _spriteRenderer;
    public GameObject _enemy;

    private readonly UnityEvent _OnDeath = new();
    private readonly UnityEvent _onHealthChanged = new();
    private void Start()
    {
        //Sprite
        _spriteRenderer.sprite = creatureData.image;

        //Health
        health = creatureData.health;
        minHealth = creatureData.minHealth;
        maxHealth = creatureData.maxHealth;

        //Defense
        defense = creatureData.defense;
        minDefense = creatureData.minDefense;
        maxDefense = creatureData.maxDefense;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(Damage(damage));
    }

    public IEnumerator Damage(float damage)
    {
        Debug.Log(damage - defense);
        health -= damage - defense;
        _spriteRenderer.color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        _spriteRenderer.color = Color.white;
        _onHealthChanged.Invoke();
        IfDead();
    } 

    private void IfDead()
    {
        if (health <= minHealth)
        {
            _OnDeath.Invoke();
            Destroy(_enemy);
        }
    }

    public UnityEvent OnDeath => _OnDeath;
    public UnityEvent OnHealthChanged => _onHealthChanged;

}
