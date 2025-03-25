using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class BigObjectEngima : MonoBehaviour
{
    [Header("BigObject")]
    [SerializeField] private BigObject _bigObject;

    [Header("Destination")]
    [SerializeField] private Destination _destination;

    [Header("Interaction")]
    [SerializeField] private bool _playerInZone;

    public bool solved;
    private readonly UnityEvent _onPuzzleResolved = new();

    private void Start()
    {
        _destination.OnPuzzleCompleted.AddListener(() => CompletedPuzzle());
    }

    public void ResetPuzzle()
    {
        _bigObject.ResetBigObject();
    }

    public void CompletedPuzzle()
    {
        solved = true;
        StartCoroutine(StableBigObject());
        _onPuzzleResolved.Invoke();
    }

    IEnumerator StableBigObject()
    {
        yield return new WaitForSecondsRealtime(2f);
        _bigObject.gameObject.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }

    public void OnResetPuzzle(InputAction.CallbackContext context)
    {
        if (context.started && _playerInZone && !solved) 
            ResetPuzzle();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
       if (!_playerInZone)
            _playerInZone = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_playerInZone)
            _playerInZone = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_playerInZone)
            _playerInZone = false;
    }
    public UnityEvent OnPuzzleSolved => _onPuzzleResolved;
}
