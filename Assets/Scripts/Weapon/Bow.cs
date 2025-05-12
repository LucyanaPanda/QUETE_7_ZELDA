using UnityEngine;

public class Bow : Weapon
{
    [SerializeField] private GameObject arrowPrefab;
    public float distance;

    public override void Attack()
    {
        _animator.SetTrigger("Attack");
        ShootArrow();
    }

    private void ShootArrow()
    {
        GameObject gameObject = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
        Arrow arrow = gameObject.GetComponent<Arrow>();
        arrow.damage = damage;
        Vector3 dir = GetDirectionTowardsMouse();
        arrow.dir = dir.normalized;
        arrow.transform.Rotate(0, 0, GetAngle(dir));
        arrow.tagParent = transform.parent.tag;
        arrow.distance = distance;
    }

    private Vector3 GetDirectionTowardsMouse()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        return dir;
    }

    private float GetAngle(Vector3 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }
}
