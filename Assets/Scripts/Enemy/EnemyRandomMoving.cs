using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyRandomMoving : MonoBehaviour
{
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private EnemyRadar _enemyRadar;
    [SerializeField] private BoxCollider2D _collider;
    private Vector2 _size;

    [Header("Moving")]
    [SerializeField] private Transform _transformBody;
    [SerializeField] private Transform _transformController;
    private Vector2 _position;
    private Vector2 _destination;
    private Vector2 _direction;
    private bool _isMoving;

    [Header("Bounds")]
    private Vector2 _topLeft;
    private Vector2 _topRight;
    private Vector2 _bottomLeft;
    private Vector2 _bottomRight;

    [Header("Time between each move")]
    [SerializeField] private float _timerMax;
    [SerializeField] private float _timerMaxReachingDestination;
    private float _timer;
    private float _timerReachingDestination;
    private void Update()
    {
        if (_timer > _timerMax && !_enemyRadar.isReturning && !_enemyRadar.isFollowingPlayer)
        {
            if (_isMoving && _timerReachingDestination < _timerMaxReachingDestination)
            {
                MoveToDestination();
                _timerMaxReachingDestination += Time.deltaTime;
            }
            else if (_isMoving && _timerReachingDestination >= _timerMaxReachingDestination)
                _isMoving = false;
            else
                RandomDestination();
        }
        else if (_timer < _timerMax)
            _timer += Time.deltaTime;
    }

    private void MoveToDestination()
    {
        _direction = _destination - (Vector2)_transformBody.position;
        _direction.Normalize();
        _transformBody.position += (Vector3)_direction * _enemyManager.creatureData.speed * Time.deltaTime;
        _enemyRadar.LookAtTarget(_destination, _transformController);
        CheckPosition();
    }

    private void CheckPosition()
    {
        float distance = Vector3.Distance(_destination, _transformBody.position);
        if (distance <= 0.2)
        {
            _timer -= _timerMax;
            _isMoving = false;
        }
    }

    private void RandomDestination()
    {
        GetBoundsCollider();
        float x = Random.Range(_topLeft.x, _topRight.x);
        float y = Random.Range(_bottomLeft.y, _topLeft.y);

        _destination = new Vector2(x, y);
        _isMoving = true;
    }

    private void GetBoundsCollider()
    {
        _size = _collider.bounds.size;
        _position = _enemyRadar._initPos;

        float top = _position.y + (_size.y / 2f);
        float btm = _position.y - (_size.y / 2f);
        float left = _position.x - (_size.x / 2f);
        float right = _position.x + (_size.x / 2f);

        _topLeft = new Vector2(left, top);
        _topRight = new Vector2(right, top);
        _bottomLeft = new Vector2(left, btm);
        _bottomRight = new Vector2(right, btm);

    }
}
