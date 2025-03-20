using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Pedestal : MonoBehaviour
{
    [SerializeField] private bool _hasturned;
    [SerializeField] private Transform _transform;
    [SerializeField] private int _currentDirection;
    [SerializeField] private int _correctDirection;
    private Vector3 _rotation = new Vector3(0, 0, 90);

    public int EnumLength;
    private readonly UnityEvent _onPedestalPressed = new();
    public bool isCorrect => _currentDirection == _correctDirection;

    private void Start()
    {
        for (int i = 0; i <= _currentDirection; i++)
        {
            UpdateDirection();
        }
    }

    private void ChangeDirection()
    {
        _currentDirection++;
        _currentDirection = (_currentDirection + EnumLength) % EnumLength;
        UpdateDirection();
        _onPedestalPressed.Invoke();
    }

    private void UpdateDirection()
    {
        _transform.eulerAngles += _rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_hasturned)
        {
            ChangeDirection();
            _hasturned = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_hasturned) 
            _hasturned = false;
    }

    public UnityEvent OnPedestalPressed => _onPedestalPressed;
}
