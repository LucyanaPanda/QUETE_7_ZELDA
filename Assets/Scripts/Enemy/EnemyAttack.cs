using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttack : MonoBehaviour
{
    [Header("EnemyManager")]
    [SerializeField] private EnemyManager _manager;

    [Header("Attack")]
    [SerializeField] private float _attack, _minAttack, _maxAttack;
    [SerializeField] private float _attackTimer, _attackMaxTimer;
    [SerializeField] private Sword _sword;

    [Header("Enemy Radar")]
    [SerializeField] private EnemyRadar _radar;

    [Header("Sounds effect")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void Start()
    {
        _attack = _manager.creatureData.attack;
        _minAttack = _manager.creatureData.minAttack;
        _maxAttack = _manager.creatureData.maxAttack;

        _attackTimer = _manager.creatureData.attackTimer;
        _attackMaxTimer = _manager.creatureData.attackMaxTimer;
    }

    private void Update()
    {
        AttackDelay();
        if (_radar.CanAttackPlayer)
        {
            OnAttack();
            Debug.Log("Attacking");
        }
    }

    public void OnAttack()
    {
        if (_attackTimer >= _attackMaxTimer)
        {
            _attackTimer -= _attackMaxTimer;
            //_audioSource.clip = _audioClip;
            //_audioSource.Play();
            _sword.gameObject.SetActive(true);
            _sword.damage = _attack;
        }
    }
    private void AttackDelay()
    {
        if (_attackTimer <= _attackMaxTimer)
        {
            _attackTimer += Time.deltaTime;
        }
    }
}
