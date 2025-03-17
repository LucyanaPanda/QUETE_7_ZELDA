using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwitchMap : MonoBehaviour
{
    [Header("Maps")]
    [SerializeField] private GameObject _outside;
    [SerializeField] private GameObject _inside;
    private bool _isInside;
    private bool _once;
    private Collider2D _path;

    private void Start()
    {
        _path = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!_once)
        {
            SwitchingMap();
            StartCoroutine(DeactivatingColliderForAFewSeconds());
            _once = true;
        }
    }

    private void SwitchingMap()
    {
        if (_isInside)
        {
            _isInside = false;
            _outside.SetActive(true);
            _inside.SetActive(false);
        }
        else
        {
            _isInside = true;
            _outside.SetActive(false);
            _inside.SetActive(true);
        }
    }

    IEnumerator DeactivatingColliderForAFewSeconds()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        _once = false;
    }

    
}
