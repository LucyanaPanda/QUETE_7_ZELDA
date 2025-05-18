using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyAttack : MonoBehaviour
{
    [Header("EnemyManager")]
    [SerializeField] private EnemyManager _manager;

    [Header("Attack")]
    [SerializeField] private float _attack, _minAttack, _maxAttack;
    [SerializeField] private float _attackTimer, _attackMaxTimer;
    [SerializeField] private Weapon _weapon;
    [SerializeField] private bool isSword;
    [SerializeField] private float distance;

    [Header("Enemy Radar")]
    [SerializeField] private EnemyRadar _radar;

    [Header("Sounds effect")]
    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {
        _attack = _manager.creatureData.attack;
        _minAttack = _manager.creatureData.minAttack;
        _maxAttack = _manager.creatureData.maxAttack;

        _attackTimer = _manager.creatureData.attackTimer;
        _attackMaxTimer = _manager.creatureData.attackMaxTimer;

        _weapon = GetComponentInChildren<Weapon>();
        _audioManager = AudioManager.Instance;
    } 

    private void Update()
    {
        AttackDelay();
        if (_radar.CanAttackPlayer)
        {
            OnAttack();
        }
    }

    public void OnAttack()
    {
        if (_attackTimer >= _attackMaxTimer)
        {
            if (distance <= 0 && !isSword) { Debug.LogError("Distance under or equal to 0, arrow will not be shot"); }

            _attackTimer -= _attackMaxTimer;
            _audioManager.PlaySound(AudioManager.AudioType.Attack);
            _weapon.damage = _attack;
            if(!isSword) { ((Slingshot)_weapon).distance = distance ; }
            _weapon.Attack();
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
