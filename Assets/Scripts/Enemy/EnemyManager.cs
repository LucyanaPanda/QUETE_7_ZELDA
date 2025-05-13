using System.Collections;
using UnityEngine;
using UnityEngine.Events;

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

    [Header("Sounds effect")]
    [SerializeField] private AudioManager _audioManager;

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

        _audioManager = AudioManager.Instance;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(Damage(damage));
    }

    public IEnumerator Damage(float damage)
    {
        Debug.Log("damage taken: " + damage + " defense: " + defense);
        health -= damage - defense;
        _spriteRenderer.color = Color.red;
        _audioManager.PlaySound(AudioManager.AudioType.Death);
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
            int moneyToGive = Random.Range(creatureData.minMoney, creatureData.maxMoney);
            PlayerInventory.Instance.AddMoney(moneyToGive);
            Destroy(_enemy);
        }
    }

    public UnityEvent OnDeath => _OnDeath;
    public UnityEvent OnHealthChanged => _onHealthChanged;

}
