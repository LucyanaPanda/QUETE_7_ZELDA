using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
public class PlayerAttack : MonoBehaviour
{
    [Header("Attack")]
    [SerializeField] private int _damage;
    [SerializeField] private float _attackTimer;
    [SerializeField] private float _attackMaxTimer;
    [SerializeField] private float _attackRange;
    [SerializeField] private GameObject _sword;
    private Transform _attackTransform;

    [Header("LayerMask")]
    [SerializeField] private LayerMask _ennemiesLayer;

    [Header("Sounds effect")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _audioClip;

    private RaycastHit2D[] hits;

    private void Start()
    {
        _attackTransform = GetComponent<Transform>();
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
            _sword.SetActive(true);
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
