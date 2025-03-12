using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Transform))]
public class PlayerController : MonoBehaviour
{
    [Header("PlayerManager")]
    [SerializeField] private PlayerManager _manager;

    [Header("Movement")]
    [SerializeField] private Transform _parentTransform;
    private float _speed, _minSpeed, _maxSpeed;
    private Vector3 _dir;

    [Header("LookAt")]
    [SerializeField] private Vector3 _lookRight;
    [SerializeField] private Vector3 _lookLeft;

    [Header("SFX")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpClip;

    private void Start()
    {
        _speed = _manager.creatureData.speed;
        _minSpeed = _manager.creatureData.minSpeed;
        _maxSpeed = _manager.creatureData.maxSpeed;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _dir.Normalize();
        _parentTransform.position += _dir * _speed * Time.deltaTime;

        // à amélirorer pour qu'on voit derrière et devant
        if (_dir.x < 0)
            LookAtDirection(false);
        else if (_dir.x > 0)
            LookAtDirection(true);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _dir = context.ReadValue<Vector2>();
    }

    // à amélirorer pour qu'on voit derrière et devant
    private void LookAtDirection(bool isRight)
    {
        if (isRight)
            _parentTransform.localScale = _lookRight;
        else if (!isRight)
            _parentTransform.localScale = _lookLeft;
    }
}

