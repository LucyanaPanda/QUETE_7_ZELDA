using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHotbar : MonoBehaviour
{
    [Header("Hotbar")]
    public static Dictionary<Item, int> hotbar = new Dictionary<Item, int>();
    public List<Item> _items = new List<Item>();
    public static readonly string hotbarSaveKey = "player_hotbar";
    public static bool addedToHotbar;
    [SerializeField] private HotbarUI _hotbarUi;
    [SerializeField] private Transform _hotbarSlotSelected;
    [SerializeField] private int _currentSlotSelected;

    [Header("Player")]
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private PlayerManager _playerManager;
    [SerializeField] private PlayerUpgradeStat _upgradeStat;

    [Header("SFX")]
    [SerializeField] private AudioManager _audioManager;

    private void Start()
    {
        _inventory = PlayerInventory.Instance;
        _audioManager = AudioManager.Instance;
    }


    public static void AddItem(Item item, int quantity)
    {
        hotbar.Add(item, quantity);
        addedToHotbar = true;
    }

    public void OnUseItem(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            foreach (KeyValuePair<Item, int> entry in _inventory.inventory)
            {
                if (entry.Key == _hotbarUi.slots[_currentSlotSelected].dragableItem.currentItem && entry.Key.canBeUse)
                {
                    _inventory.inventory[entry.Key] = entry.Value - 1;
                    hotbar[entry.Key] = entry.Value - 1;

                    if (hotbar[entry.Key] <= 0)
                    {
                        _inventory.inventory.Remove(entry.Key);
                        hotbar.Remove(entry.Key);
                        _hotbarUi.slots[_currentSlotSelected].dragableItem.currentItem = null;
                    }

                    if (entry.Key.isPotion)
                        _playerManager.ActivateBoost(entry.Key);
                    else
                        _upgradeStat.UseSpecialItem(entry.Key);

                    _audioManager.PlaySound(AudioManager.AudioType.Potion);

                    _hotbarUi.slots[_currentSlotSelected].UpdateInformation();
                    SaveInventory.Instance.SaveTheInventory();
                    break;
                }
            }
        }
    }


    #region Scrolling system
    public void OnScroll(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _currentSlotSelected += (int)context.ReadValue<float>();
            _currentSlotSelected = (_currentSlotSelected + 4) % 4;
            ChangeSelectedSlot();
        }
    }

    private void ChangeSelectedSlot()
    {
        _hotbarSlotSelected.SetParent(_hotbarUi.slots[_currentSlotSelected].transform);
        _hotbarSlotSelected.localPosition = Vector3.zero;
    }
    #endregion
}
