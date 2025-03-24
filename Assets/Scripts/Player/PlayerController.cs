using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Transform))]
public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerManager _player;
    [SerializeField] private PlayerInteractions _playerInteractions;

    [Header("Movement")]
    [SerializeField] private Transform _parentTransform;
    private Vector3 _dir;

    [Header("LookAt")]
    [SerializeField] private Vector3 _lookRight;
    [SerializeField] private Vector3 _lookLeft;

    [Header("SFX")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpClip;

    private void Start()
    {
        _parentTransform.position = _player._spawnpoint;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _dir.Normalize();
        _parentTransform.position += _dir * _player.speed * Time.deltaTime;

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

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
            _playerInteractions.TryInteract();
    }
}

