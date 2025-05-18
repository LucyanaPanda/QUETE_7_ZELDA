using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Transform))]
public class PlayerAttack : MonoBehaviour
{
    [Header("PlayerManager")]
    [SerializeField] private PlayerManager _player;

    [Header("Weapon Slot")]
    [SerializeField] private Slot _weaponSlot;

    [Header("Attack")]
    [SerializeField] private GameObject _weaponPosition;
    [SerializeField] private Weapon _swordPrefab;
    [SerializeField] private Weapon _bowPrefab;
    private Weapon _currentWeapon;

    [Header("DialogueManager")]
    [SerializeField] private DialogueManager _dialogueManager;

    [Header("GameManager")]
    [SerializeField] private GameManager _gameManager;

    private AudioManager _audioManager;

    private void Start()
    {
        _audioManager = AudioManager.Instance;
    }

    private void Update()
    {
        AttackDelay();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (_player.attackTimer >= _player.attackMaxTimer && !_dialogueManager.dialoguePlayed && _gameManager.playerInGame)
        {
            GetWeaponFromSlot();
            _player.attackTimer -= _player.attackMaxTimer;
            _currentWeapon.damage = _player.attack;
            _currentWeapon.Attack();
            _audioManager.PlaySound(AudioManager.AudioType.Attack);
            Debug.Log("Attacking");
        }
    }

    private void AttackDelay()
    {
        if (_player.attackTimer <= _player.attackMaxTimer)
        {
            _player.attackTimer += Time.deltaTime;
        }
    }

    private void GetWeapon()
    {
        _currentWeapon = GetComponentInChildren<Weapon>();
    }

    private void GetWeaponFromSlot()
    {
        if (_currentWeapon == null)
        {
            if (_weaponSlot.dragableItem.currentItem == null)
            {
                Instantiate(_swordPrefab, _weaponPosition.transform, false);
                GetWeapon();
                return;
            } else
            {
                if (_weaponSlot.dragableItem.currentItem.isSword)
                {
                    Instantiate(_swordPrefab, _weaponPosition.transform, false);
                    GetWeapon();
                    return;
                }
                else
                {
                    Weapon bow = Instantiate(_bowPrefab, _weaponPosition.transform, false);
                    ((Bow)bow).distance = _weaponSlot.dragableItem.currentItem.distance;
                    ((Bow)bow).fromPlayer = true;
                    GetWeapon();
                    return;
                }
            }
        }
        else if (_currentWeapon != null && _weaponSlot.dragableItem.currentItem != null)
        {
            Sword sword = _currentWeapon.GetComponent<Sword>();
            Bow bow = _currentWeapon.GetComponent<Bow>();

            if (sword != null && _weaponSlot.dragableItem.currentItem.isSword) { GetWeapon(); return; }
            else if (bow != null && !_weaponSlot.dragableItem.currentItem.isSword) { GetWeapon(); return; }
        }

        foreach (Transform child in _weaponPosition.transform) { Destroy(child.gameObject); }

        if (_weaponSlot.dragableItem.currentItem.isSword)
            {if (_weaponSlot.dragableItem.currentItem.isSword)
            {
                Instantiate(_swordPrefab, _weaponPosition.transform, false);
            }
            else
            {
                Weapon bow = Instantiate(_bowPrefab, _weaponPosition.transform, false);
                ((Bow)bow).distance = _weaponSlot.dragableItem.currentItem.distance;
                ((Bow)bow).fromPlayer = true;
            }
        }
    }

}
