using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Animator))]
public class FadeInOut : MonoBehaviour
{
    private readonly UnityEvent _onEvent = new();

    public  Image _imageFade;
    public  Animator _animatorFade;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _imageFade = GetComponent<Image>();
        _animatorFade = GetComponent<Animator>();
    }

    // Update is called once per frame
    public void FadeInAndOut()
    {
        StartCoroutine(PlayAnimation());
    }

    IEnumerator PlayAnimation()
    {
        _animatorFade.Play("FadeIn");
        yield return new WaitForSecondsRealtime(1f);
        _onEvent.Invoke();
        _animatorFade.Play("FadeOut");
        
    }

    public UnityEvent OnEvent => _onEvent;
}
