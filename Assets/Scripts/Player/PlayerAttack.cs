using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
public class PlayerAttack : MonoBehaviour
{
    [Header("PlayerManager")]
    [SerializeField] private PlayerManager _player;

    [Header("Attack")]
    [SerializeField] private Sword _sword;

    [Header("Sounds effect")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private void Update()
    {
        AttackDelay();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (_player.attackTimer >= _player.attackMaxTimer)
        {
            _player.attackTimer -= _player.attackMaxTimer;
            _audioSource.clip = _audioClip;
            _audioSource.Play();
            _sword.gameObject.SetActive(true);
            _sword.damage = _player.attack;
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
