using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
public class PlayerAttack : MonoBehaviour
{
    [Header("PlayerManager")]
    [SerializeField] private PlayerManager _player;

    [Header("Attack")]
    [SerializeField] private Sword _sword;

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
        if (_player.attackTimer >= _player.attackMaxTimer)
        {
            _player.attackTimer -= _player.attackMaxTimer;
            _sword.gameObject.SetActive(true);
            _sword.damage = _player.attack;
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
}
