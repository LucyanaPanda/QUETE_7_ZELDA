using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Transform))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed;
    [SerializeField] private Transform _parentTransform;
    private Vector3 _dir;
    private Transform _transformBody;

    [Header("LookAt")]
    [SerializeField] private Vector3 _lookRight;
    [SerializeField] private Vector3 _lookLeft;

    [Header("SFX")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _jumpClip;

    private void Start()
    {
        _transformBody = GetComponent<Transform>();
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
            _transformBody.localScale = _lookRight;
        else if (!isRight)
            _transformBody.localScale = _lookLeft;

    }
}
