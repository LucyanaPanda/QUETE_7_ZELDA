using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory")]
    [SerializeField] private GameObject _inventoryPanel;
    private bool _inventoryVisible = false;

    [Header("PauseManager")]
    [SerializeField] private PauseManager _pauseManager;

    public void ShowHideInventory(InputAction.CallbackContext context)
    {
        if (_inventoryVisible)
        {
            _inventoryPanel.SetActive(false);
            _inventoryVisible = false;
            _pauseManager.ResumeGame();
        }
        else
        {
            _inventoryPanel.SetActive(true);
            _inventoryVisible = true;
            _pauseManager.PauseGame();
        }
    }
}
