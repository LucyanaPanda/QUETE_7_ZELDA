using System.Collections;
using Unity.Profiling;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;

    public float damage;
    public float distance;
    public Vector3 dir;
    public string tagParent;

    private Vector3 initPos;

    private void Awake()
    {
        initPos = transform.position;
    }

    private void Update()
    {
        _transform.position += dir * _speed * Time.deltaTime;
        if (Vector2.Distance(transform.position, initPos) >= distance) { Destroy(this.gameObject); }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable iDamageable = collision.GetComponent<IDamageable>();
        if (iDamageable != null && tagParent != "" && collision.tag != tagParent)
        {
            iDamageable.TakeDamage(damage);
        }
    }

}
