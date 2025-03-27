using UnityEngine;
using UnityEngine.UI;

public class HPBarPlayer : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _playerManager.OnHealthChanged.AddListener(() => UpdateBar());
    }

    private void UpdateBar()
    {
        float value = _playerManager.health / _playerManager.maxHealth;
        _slider.value = value;
    }
}
