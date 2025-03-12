using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(CircleCollider2D))]
public class EnemyRadar : MonoBehaviour
{
    [Header("EnemyManager")]
    [SerializeField] private EnemyManager _manager;

    [Header("Movement")]
    [SerializeField] private Transform _transformBody;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _minDist;
    private float _speed;
    private bool _isReturning;
    public bool CanAttackPlayer;

    [Header("Radius of Radar")]
    [SerializeField] private float _rad;
    [SerializeField] private CircleCollider2D _collider;

    //Initial Position
    private Vector3 _initPos;

    [Header("LookAt")]
    [SerializeField] private Vector3 _lookRight;
    [SerializeField] private Vector3 _lookLeft;


    private void Start()
    {
        _collider.radius = _rad;
        _speed = _manager.creatureData.speed;
    }

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
        float distance = Vector3.Distance(_target.position, _transformBody.position);
        if (distance > _minDist)
        {
            Vector3 pos = Vector3.MoveTowards(_transformBody.position, _target.position, _speed * Time.deltaTime);
            _rb.MovePosition(pos);
            LookAtTarget(_target.position);
            CanAttackPlayer = false;
        }
        else
        {
            CanAttackPlayer = true;
        }
    }

    private void ReturnToInitPos()
    {
        Vector3 pos = Vector3.MoveTowards(_transformBody.position, _initPos, _speed * Time.deltaTime);
        if (pos == Vector3.zero)
            _isReturning = false;
        _rb.MovePosition(pos);
        LookAtTarget(_initPos);
        CanAttackPlayer = false;
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
