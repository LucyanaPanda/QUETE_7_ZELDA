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
    [SerializeField] private Weapon _swordPrefab;
    [SerializeField] private Weapon _bowPrefab;
    private Weapon _currentWeapon;

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
        if (_player.attackTimer >= _player.attackMaxTimer && !DialogueManager.Instance.dialoguePlayed)
        {
            GetWeaponFromSlot();
            GetWeapon();
            _player.attackTimer -= _player.attackMaxTimer;
            _currentWeapon.damage = _player.attack;
            _currentWeapon.Attack();
            _audioManager.PlaySound(AudioManager.AudioType.Attack);
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
        try
        {
            _currentWeapon = GetComponentInChildren<Weapon>();
            Debug.Log(_currentWeapon);
        } catch (Exception e ) { Debug.Log(e); }
        
    }

    private void GetWeaponFromSlot()
    {
        if (_currentWeapon == null)
        {
            if (_weaponSlot.dragableItem.currentItem == null)
            {
                Instantiate(_swordPrefab, transform, false);
                Debug.Log("Test 1");
                return;
            } else
            {
                if (_weaponSlot.dragableItem.currentItem.isSword)
                {
                    Instantiate(_swordPrefab, transform, false);
                    Debug.Log("Test 89");
                    return;
                }
                else
                {
                    Weapon bow = Instantiate(_bowPrefab, transform, false);
                    ((Bow)bow).distance = _weaponSlot.dragableItem.currentItem.distance;
                    Debug.Log("Test 99");
                    return;
                }
            }
        }
        else if (_currentWeapon != null && _weaponSlot.dragableItem.currentItem != null)
        {
            Sword sword = _currentWeapon.GetComponent<Sword>();
            Bow bow = _currentWeapon.GetComponent<Bow>();

            if (sword != null && _weaponSlot.dragableItem.currentItem.isSword) { return; }
            else if (bow != null && !_weaponSlot.dragableItem.currentItem.isSword) { return; }

            Debug.Log("Test 2, not found weapon syncro");
        }

        foreach (Transform child in transform) { Destroy(child.gameObject); }
        Debug.Log("Destroyed");
        if (_weaponSlot.dragableItem.currentItem.isSword)
            {if (_weaponSlot.dragableItem.currentItem.isSword)
            {
                Instantiate(_swordPrefab, transform, false);
                Debug.Log("Test 3");
            }
            else
            {
                Weapon bow = Instantiate(_bowPrefab, transform, false);
                ((Bow)bow).distance = _weaponSlot.dragableItem.currentItem.distance;
                Debug.Log("Test 4");
            }
        }
        
    }

}
