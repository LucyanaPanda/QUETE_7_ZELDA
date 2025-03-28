using UnityEngine;
using UnityEngine.UI;

public class HPBarEnemy : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private RectTransform _transformImage;

    private Vector3 _scale = Vector3.one;

    private void Start()
    {
        _enemyManager.OnHealthChanged.AddListener(() => UpdateBar());
    }

    private void UpdateBar()
    {
        float value = _enemyManager.health / _enemyManager.maxHealth;
        _scale.x = value;
        _transformImage.localScale = _scale;
    }
}
