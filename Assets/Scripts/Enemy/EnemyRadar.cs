using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyRadar : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform _transformBody;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private Vector2 _dir;
    [SerializeField] private bool _isReturning;
    [SerializeField] private float _minDist;

    [Header("Initial Position")]
    [SerializeField] private Vector3 _initPos;

    [Header("LookAt")]
    [SerializeField] private Vector3 _lookRight;
    [SerializeField] private Vector3 _lookLeft;

    private void Update()
    {
        if (_isReturning)
            ReturnToInitPos();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            FollowPlayer(collision.transform);
            _isReturning = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _isReturning = true;
        }
    }

    private void FollowPlayer(Transform _target)
    {
        Vector3 pos = Vector3.MoveTowards(_transformBody.position, _target.position, _speed * Time.deltaTime);
        _rb.MovePosition(pos);
        LookAtTarget(_target.position);
    }

    private void ReturnToInitPos()
    {
        Vector3 pos = Vector3.MoveTowards(_transformBody.position, _initPos, _speed * Time.deltaTime);
        if (pos == Vector3.zero)
            _isReturning = false;
        _rb.MovePosition(pos);
        LookAtTarget(_initPos);
    }

    private void LookAtTarget(Vector3 destination)
    {
        Vector2 distance = destination - _transformBody.position;
        if (distance.x > 0)
            _transformBody.localScale = _lookRight;
        else if (distance.x < 0)
            _transformBody.localScale = _lookLeft;
    }
}
