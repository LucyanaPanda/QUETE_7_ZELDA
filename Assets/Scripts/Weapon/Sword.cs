using UnityEngine;

public class Sword : Weapon
{

    public override void Attack()
    {
        _animator = GetComponent<Animator>();
        _animator.SetTrigger("Attack");
    }

}
