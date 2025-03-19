using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [Header("Data")]
    public Creature creatureData;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public float health, minHealth, maxHealth;
    public float attackTimer, attackMaxTimer;
    public float attack, minAttack, maxAttack;
    public float defense, minDefense, maxDefense;
    public float speed, minSpeed, maxSpeed;

    private void Start()
    {
        //Sprite
        _spriteRenderer.sprite = creatureData.image;

        //Health
        health = creatureData.health;
        minHealth = creatureData.minHealth;
        maxHealth = creatureData.maxHealth;

        //Attack
        attack = creatureData.attack;
        minAttack = creatureData.minAttack;
        maxAttack = creatureData.maxAttack;

        attackTimer = creatureData.attackTimer;
        attackMaxTimer = creatureData.attackMaxTimer;

        //Defense
        defense = creatureData.defense;
        minDefense = creatureData.minDefense;
        maxDefense = creatureData.maxDefense;

        //Speed
        speed = creatureData.speed;
        minSpeed = creatureData.minSpeed;
        maxSpeed = creatureData.maxSpeed;
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
        IfDead();
    }

    private void IfDead()
    {
        if (health < minHealth)
            Destroy(gameObject);
    }
}
