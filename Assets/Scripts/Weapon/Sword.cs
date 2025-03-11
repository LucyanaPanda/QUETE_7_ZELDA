using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Sword : MonoBehaviour
{
    private Animator _animator;

    void OnEnable()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine(LaunchAnimationAttack());
    }

    IEnumerator LaunchAnimationAttack()
    {
        _animator.SetTrigger("Attack");
        yield return new WaitForSecondsRealtime(0.4f);
        this.gameObject.SetActive(false);

    }
}
