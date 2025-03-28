using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("Potion Effect")]
    public bool _attackBoostOn;
    public bool _speedBoostOn;
    public bool _defenseBoostOn;

    public float _attackBoostDuration;
    public float _attackBoostTimer;
    public float _attackBoost;

    public float _speedBoostDuration;
    public float _speedBoostTimer;
    public float _speedBoost;

    public float _defenseBoostDuration;
    public float _defenseBoostTimer;
    public float _defenseBoost;


    [Header("FadeInOut")]
    [SerializeField] private Image image;
    [SerializeField] private Animator _animatorImage;

    [Header("Map")]
    [SerializeField] private List<GameObject> _mapsOutsides;
    [SerializeField] private List<GameObject> _mapsHidden;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private readonly UnityEvent _onHealthChanged = new();

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

            _onHealthChanged.Invoke();
        }

        transform.position = _spawnpoint;
        _onHealthChanged.Invoke();
    }

    private void Update()
    {
        if (_attackBoostOn)
            Boost(ref _attackBoostOn, ref _attackBoostDuration, ref _attackBoostTimer, ref _attackBoost, ref attack);
        if (_defenseBoostOn)
            Boost(ref _defenseBoostOn, ref _defenseBoostDuration, ref _defenseBoostTimer, ref _defenseBoost, ref defense);
        if (_speedBoostOn)
            Boost(ref _speedBoostOn, ref _speedBoostDuration, ref _speedBoostTimer, ref _speedBoost, ref speed);
    }

    public void TakeDamage(float damage)
    {
        StartCoroutine(Damage(damage));
    }

    public IEnumerator Damage(float damage)
    {
        Debug.Log(damage - defense);
        health -= damage - defense;
        _onHealthChanged.Invoke();
        _spriteRenderer.color = Color.red;
        yield return new WaitForSecondsRealtime(0.5f);
        _spriteRenderer.color = Color.white;
        StartCoroutine(IfDead());
    }

    public IEnumerator  IfDead()
    {
        if (health <= minHealth)
        {
            _animatorImage.Play("FadeIn");
            yield return new WaitForSecondsRealtime(1f);
            ResetMap();
            transform.position = _spawnpoint;
            health = maxHealth;
            _animatorImage.Play("FadeOut");
            _onHealthChanged.Invoke();
        }
        yield return null;
    }

    public IEnumerator OnFade()
    {
        _animatorImage.Play("FadeIn");
        yield return new WaitForSecondsRealtime(1f);
        _animatorImage.Play("FadeOut");
    }

    private void ResetMap()
    {
        foreach (GameObject map in _mapsOutsides)
        {
            map.SetActive(true);
        }

        foreach(GameObject map in _mapsHidden)
        {
            map.SetActive(false);
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

    private void Boost(ref bool boostOn, ref float boostDuration, ref float boostTimer, ref float boost, ref float stat)
    {
        boostTimer += Time.deltaTime;
        if (boostTimer >= boostDuration)
        {
            boostTimer = 0;
            boostOn = false;
            stat -= boost;
            boost = 0;
            boostDuration = 0;

        }
    }

    public void ActivateBoost(Item potion)
    {
        switch (potion.name)
        {
            case "AttackPotion":
                OnActivateBoost(ref _attackBoostOn, ref _attackBoostDuration, ref _attackBoost, ref attack, potion.attackBoost, potion.duration);
                break;
            case "DefensePotion":
                OnActivateBoost(ref _defenseBoostOn, ref _defenseBoostDuration, ref _defenseBoost, ref defense, potion.defBoost, potion.duration);
                break;
            case "Speed Potion":
                OnActivateBoost(ref _speedBoostOn, ref _speedBoostDuration, ref _speedBoost, ref speed, potion.speedBoost, potion.duration);
                break;
            case "HealthPotion":
                health = Mathf.Min(health+potion.healthBoost, maxHealth);
                _onHealthChanged.Invoke();
                break;
        }
    }

    private void OnActivateBoost(ref bool boostOn, ref float boostDuration, ref float boost, ref float stat, float boostValue, float boostItemDuration)
    {
        if (boostOn)
        {
            boostDuration += boostItemDuration;
            return;
        }

        boostOn = true;
        boostDuration = boostItemDuration;
        boost = boostValue;
        stat += boost;
    }

    public UnityEvent OnHealthChanged => _onHealthChanged;
}
