using System.Collections;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private Transform _transform;
    [SerializeField] private float _speed;

    public float damage;
    public Vector3 dir;

    private IDamageable notToTarget;
    private bool hitOnce;

    private void Start()
    {
        StartCoroutine(DestroyHimself());
    }

    private void Update()
    {
        _transform.position += dir * _speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable iDamageable = collision.GetComponent<IDamageable>();
        if (iDamageable != null && iDamageable != notToTarget && hitOnce)
        {
            iDamageable.TakeDamage(damage);
        }
        else
        {
            notToTarget = iDamageable;
            hitOnce = true;
        }
    }

    IEnumerator DestroyHimself()
    {
        yield return new WaitForSecondsRealtime(7);
        Destroy(gameObject);
    }

    IEnumerator WaitForDirection()
    {
        if (dir ==  Vector3.zero)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            StartCoroutine(WaitForDirection());
        }
        else
        {
            Quaternion.LookRotation(dir);
            Debug.Log("Bing Bong");
        }
    }
}
