using UnityEditor;
using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private GameObject arrowPrefab;
    public override void Attack()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Attack");
        ShootArrow();
    }

    private void ShootArrow()
    {
        Instantiate(arrowPrefab, transform.position, transform.rotation);
    }
}
