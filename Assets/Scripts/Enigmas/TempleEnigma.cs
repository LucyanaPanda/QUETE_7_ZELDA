using Unity.VisualScripting;
using UnityEngine;

public class TempleEnigma : MonoBehaviour
{
    [Header("Enigmas")]
    [SerializeField] private BigObjectEngima _bigObjectEngima;
    [SerializeField] private ElectricalManager _electricalManager;

    [Header("Wall")]
    [SerializeField] private Animator _wallAnimator;

    public static bool templeEnigmaResolved;

    private void Start()
    {

        _bigObjectEngima.OnPuzzleSolved.AddListener(() => CheckIfResolved());
        _electricalManager.OnPuzzleSolved.AddListener(() => CheckIfResolved());
    }

    private void CheckIfResolved()
    {
        if (_bigObjectEngima.solved && _electricalManager.solved)
        {
            _wallAnimator.enabled = true;
            templeEnigmaResolved = true;
            Destroy(_wallAnimator.gameObject, 1f);
        }
    }


}
