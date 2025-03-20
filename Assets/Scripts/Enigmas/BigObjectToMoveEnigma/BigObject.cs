using UnityEngine;

public class BigObject : MonoBehaviour
{
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector2 _initPos;

    private void Start()
    {
        _initPos = _transform.localPosition;
    }

    public void ResetBigObject()
    {
        _transform.localPosition = _initPos; 
    }
}
