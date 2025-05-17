using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(Transform))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Player")]
    [SerializeField] private PlayerManager _player;
    [SerializeField] private PlayerInteractions _playerInteractions;

    [Header("Movement")]
    [SerializeField] private Transform _parentTransform;
    private Vector3 _dir;

    [Header("LookAt")]
    [SerializeField] private Vector3 _lookRight;
    [SerializeField] private Vector3 _lookLeft;

    [Header("Reveal Ennemies")]
    [SerializeField] private LayerMask _targetLayerMask;
    [SerializeField] private float _radius;
    [SerializeField] private Item _itemRevealer;
    private PlayerInventory _inventory;
    private void Awake()
    {
        if (Instance != null) { Destroy(this); }
        else {  Instance = this; }
    }

    private void Start()
    {
        _inventory = GetComponentInParent<PlayerInventory>();
        _parentTransform.position = _player._spawnpoint;
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        _dir.Normalize();
        _parentTransform.position += _dir * _player.speed * Time.deltaTime;

        // à amélirorer pour qu'on voit derrière et devant
        if (_dir.x < 0)
            LookAtDirection(false);
        else if (_dir.x > 0)
            LookAtDirection(true);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        _dir = context.ReadValue<Vector2>();
    }

    // à amélirorer pour qu'on voit derrière et devant
    private void LookAtDirection(bool isRight)
    {
        if (isRight)
            _parentTransform.localScale = _lookRight;
        else if (!isRight)
            _parentTransform.localScale = _lookLeft;
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
            _playerInteractions.TryInteract();

    }

    public void OnReveal(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_inventory.inventory.ContainsKey(_itemRevealer))
            {
                Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, _radius, _targetLayerMask);

                foreach (Collider2D target in targets)
                {
                    target.GetComponent<EnemyHiddenReveal>().RevealBody();
                }
            }
        }
    }
}

