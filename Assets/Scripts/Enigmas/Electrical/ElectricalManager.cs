using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ElectricalManager : MonoBehaviour
{
    [Header("Pedestales")]
    [SerializeField] private List<Pedestal> _pedestals;
    private int _enumLength;

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
        //Invoke the recompsense function
    }

    enum Direction
    {
        North,
        South,
        East,
        West
    }
}
