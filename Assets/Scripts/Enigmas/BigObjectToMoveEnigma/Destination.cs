using UnityEngine;
using UnityEngine.Events;

public class Destination : MonoBehaviour
{
    private readonly UnityEvent _onPuzzleCompleted = new();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "BigObject")
        {
            _onPuzzleCompleted.Invoke();    
        }
    }

    public UnityEvent OnPuzzleCompleted => _onPuzzleCompleted;
}
