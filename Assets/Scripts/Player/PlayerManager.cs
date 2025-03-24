using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour, IDamageable
{
    [Header("Data")]
    public Creature creatureData;
    public float health, minHealth, maxHealth;
    public float attackTimer, attackMaxTimer;
    public float attack, minAttack, maxAttack;
    public float defense, minDefense, maxDefense;
    public float speed, minSpeed, maxSpeed;
    public Vector3 _spawnpoint;
    public readonly static string playerDataSaveKey = "PlayerData";

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private readonly UnityEvent _onDamageTaken = new();

    private void Start()
    {
        if (!LoadPlayerData())
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

            //Spawnpoint
            _spawnpoint = transform.position;

            _onDamageTaken.Invoke();
        }

        transform.position = _spawnpoint;
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(Damage(damage));
    }

    public IEnumerator Damage(float damage)
    {
        Debug.Log(damage - defense);
        health -= damage - defense;
        _onDamageTaken.Invoke();
        _spriteRenderer.color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        _spriteRenderer.color = Color.white;
        IfDead();
    }

    private void IfDead()
    {
        if (health < minHealth)
        {
            transform.position = _spawnpoint;
            health = maxHealth;
        }
    }

    public void UseSpawnpoint(Vector3 pos)
    {
        _spawnpoint = pos;
        SavePlayerData();
    }

    public void SavePlayerData()
    {
        PlayerData playerData = new PlayerData();

        //CreatureData
        playerData.creatureData = creatureData;

        //Health
        playerData.health = health;
        playerData.minHealth = minHealth;
        playerData.maxHealth = maxHealth;

        //Attack
        playerData.attack = attack;
        playerData.minAttack = minAttack;
        playerData.maxAttack = maxAttack;

        //Defense
        playerData.defense = defense;
        playerData.minDefense = minDefense;
        playerData.maxDefense = maxDefense;

        //Speed
        playerData.speed = speed;
        playerData.minSpeed = minSpeed;
        playerData.maxSpeed = maxSpeed;

        //Spawnpoint
        playerData.spawnpoint = _spawnpoint;
      

        string json = JsonUtility.ToJson(playerData);
        PlayerPrefs.SetString(playerDataSaveKey, json);
        PlayerPrefs.Save();

        Debug.Log("Saved PlayerData successfully");
    }

    public bool LoadPlayerData()
    {
        if (PlayerPrefs.HasKey(playerDataSaveKey))
        {
            string json = PlayerPrefs.GetString(playerDataSaveKey);
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

            //Creature Data
            creatureData = playerData.creatureData;

            //Health
            health = playerData.health;
            minHealth = playerData.minHealth;
            maxHealth = playerData.maxHealth;

            //Attack
            attack = playerData.attack;
            minAttack = playerData.minAttack;
            maxAttack = playerData.maxAttack;

            //Defense
            defense = playerData.defense;
            minDefense = playerData.minDefense;
            maxDefense = playerData.maxDefense;

            //Speed
            speed = playerData.speed;
            minSpeed = playerData.minSpeed;
            maxSpeed = playerData.maxSpeed;

            //Spawnpoint
            _spawnpoint = playerData.spawnpoint;

            Debug.Log(_spawnpoint);
            Debug.Log("Loaded data successful");
            return true;
        }

        return false;
    }

    public UnityEvent OnDamageTaken => _onDamageTaken;
}
