using UnityEngine;

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
    public bool isReturning;
    public bool isFollowingPlayer;
    public bool CanAttackPlayer;
    public Vector3 _initPos;

    [Header("Radius of Radar")]
    [SerializeField] private float _rad;
    [SerializeField] private CircleCollider2D _collider;

    [Header("LookAt")]
    [SerializeField] private Vector3 _lookRight;
    [SerializeField] private Vector3 _lookLeft;


    private void Start()
    {
        _collider.radius = _rad;
        _speed = _manager.creatureData.speed;
        _initPos = transform.position;
    }

    private void Update()
    {
        if (isReturning)
            ReturnToInitPos();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PlayerManager playermanager = collision.gameObject.GetComponent<PlayerManager>();
        if (playermanager != null)
        {
            FollowPlayer(collision.transform);
            isReturning = false;
            isFollowingPlayer = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerManager playerManager = collision.gameObject.GetComponent<PlayerManager>();
        if (playerManager != null)
        {
            isReturning = true;
        }
    }

    private void FollowPlayer(Transform _target)
    {
        float distance = Vector3.Distance(_target.position, _transformBody.position);
        if (distance > _minDist)
        {
            Vector3 pos = Vector3.MoveTowards(_transformBody.position, _target.position, _speed * Time.deltaTime);
            _rb.MovePosition(pos);
            LookAtTarget(_target.position, _transformBody);
            CanAttackPlayer = false;
        }
        else
            CanAttackPlayer = true;
    }

    private void ReturnToInitPos()
    {
        Vector3 pos = Vector3.MoveTowards(_transformBody.position, _initPos, _speed * Time.deltaTime);
        if (pos == Vector3.zero)
            isReturning = false;
        _rb.MovePosition(pos);
        LookAtTarget(_initPos, _transformBody);
        CanAttackPlayer = false;
        isFollowingPlayer = false;
    }

    public void LookAtTarget(Vector3 destination, Transform transform)
    {
        Vector2 distance = destination - transform.position;
        if (distance.x > 0)
            transform.localScale = _lookRight;
        else if (distance.x < 0)
            transform.localScale = _lookLeft;
    }
}
