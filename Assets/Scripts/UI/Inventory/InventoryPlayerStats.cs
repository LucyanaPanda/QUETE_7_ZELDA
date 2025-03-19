using TMPro;
using UnityEngine;

public class InventoryPlayerStats : MonoBehaviour
{
    [Header("Player infos")]
    [SerializeField] private PlayerManager _player;

    [Header("Display infos")]
    [SerializeField] private TMP_Text _health;
    [SerializeField] private TMP_Text _attack;
    [SerializeField] private TMP_Text _defense;
    [SerializeField] private TMP_Text _speed;

    private void OnEnable()
    {
        UpdateStatsUI();
    }

    public void UpdateStatsUI()
    {
        _health.text = "Health: " + _player.health;
        _attack.text = "Attack: " + _player.attack;
        _defense.text = "Defense: " + _player.defense;
        _speed.text = "Speed: " + _player.speed;
    }
}
