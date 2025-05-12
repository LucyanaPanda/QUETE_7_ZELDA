using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
public class PlayerAttack : MonoBehaviour
{
    [Header("PlayerManager")]
    [SerializeField] private PlayerManager _player;

    [Header("Attack")]
    [SerializeField] private Weapon _weapon;

    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        AttackDelay();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (_player.attackTimer >= _player.attackMaxTimer && !DialogueManager.Instance.dialoguePlayed)
        {
            GetWeapon();
            _player.attackTimer -= _player.attackMaxTimer;
            _weapon.damage = _player.attack;
            _weapon.Attack();
            _audioManager.PlaySound(AudioManager.AudioType.Attack);
        }
    }

    private void AttackDelay()
    {
        if (_player.attackTimer <= _player.attackMaxTimer)
        {
            _player.attackTimer += Time.deltaTime;
        }
    }

    private void GetWeapon()
    {
        _weapon = GetComponentInChildren<Weapon>();
        Debug.Log(_weapon);
    }

    private bool IsSword()
    {
        Sword sword = _weapon.GetComponent<Sword>();
        if (sword != null) { return true; }
        return false;

    }
}
