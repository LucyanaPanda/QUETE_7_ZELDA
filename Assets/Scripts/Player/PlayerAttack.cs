using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
public class PlayerAttack : MonoBehaviour
{
    [Header("PlayerManager")]
    [SerializeField] private PlayerManager _manager;

    [Header("Attack")]
    [SerializeField] private Sword _sword;
    private float _attackTimer, _attackMaxTimer;
    private float _attack, _minAttack, _maxAttack;

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
    }

    public void OnAttack(InputAction.CallbackContext context)
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
