using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ElectricalManager : MonoBehaviour
{
    [Header("Pedestales")]
    [SerializeField] private List<Pedestal> _pedestals;
    private int _enumLength;

    public bool solved;
    private readonly UnityEvent _onPuzzleResolved = new();

    private void Start()
    {
        _enumLength = System.Enum.GetValues(typeof(Direction)).Length;
        foreach (Pedestal pedestal in _pedestals)
        {
            pedestal.OnPedestalPressed.AddListener(() => CheckIfCorrect());
            pedestal.EnumLength = _enumLength;
        }
    }

    private void CheckIfCorrect()
    {
        foreach (Pedestal pedestal in _pedestals)
        {
            if (!pedestal.isCorrect)
                return;
        }

        Debug.Log("Puzzle Resolved");
        solved = true;
        _onPuzzleResolved.Invoke();
    }

    enum Direction
    {
        North,
        South,
        East,
        West
    }

    public UnityEvent OnPuzzleSolved => _onPuzzleResolved;
}
