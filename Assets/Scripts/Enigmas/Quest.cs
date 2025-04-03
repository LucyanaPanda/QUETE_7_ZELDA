using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [Header("NPC Involved")]
    [SerializeField] private NPCDialogue _questGiver;

    [Header("TypeOfQuest")]
    [SerializeField] private bool _objectToGive;
    [SerializeField] private bool _monstersToKill;
    [SerializeField] private bool _questResolved;

    [SerializeField] private List<string> _dialogueBeginning;
    [SerializeField] private List<string> _dialogueEnd;

    [Header("Object to bring back")]
    [SerializeField] private Item _questObject;

    [Header("Monsters To Kill")]
    [SerializeField] private List<GameObject> _questEnemies;

    [SerializeField] private PlayerInventory _playerInventory;

    [Header("Path To Block")]
    [SerializeField] private GameObject _pathToblock;
    [SerializeField] private bool _hasAPathBlocked;

    private void Start()
    {
        _questGiver.dialogueLines = _dialogueBeginning;
    }

    public void IfQuestResolved()
    {
        /*if (_objectToGive && _monstersToKill && !_questResolved)
        {
            foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
            {
                if (entry.Key == _questObject)
                {
                    if (entry.Key.isPotion || entry.Key.isWeapon || entry.Key.isAccesorie || entry.Key.isArmor)
                    {
                        PlayerInventory.inventory[entry.Key] = entry.Value - 1;
                        if (PlayerInventory.inventory[entry.Key] <= 0)
                            PlayerInventory.inventory.Remove(entry.Key);
                    }

                    else
                        PlayerInventory.inventory.Remove(entry.Key);
                    _playerInventory.DisplayInventory();
                    _playerInventory.SaveInventory();
                    _playerInventory.LoadInventory();
                }
            }

            for (int i = 0; i < _questEnemies.Count; i++)
            {
                if (_questEnemies[i] != null)
                {
                    return;
                }
            }

            _questGiver.dialogueLines = _dialogueEnd;
            _questResolved = true;
            if (_hasAPathBlocked)
                _pathToblock.SetActive(false);
        }
        else*/ if (_objectToGive && !_monstersToKill && !_questResolved)
        {
            foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
            {
                if (entry.Key == _questObject)
                {
                    _questGiver.dialogueLines = _dialogueEnd;
                    _questResolved = true;
                    if (entry.Key.isPotion || entry.Key.isWeapon || entry.Key.isAccesorie || entry.Key.isArmor)
                    {
                        PlayerInventory.inventory[entry.Key] = entry.Value - 1;
                        if (PlayerInventory.inventory[entry.Key] <= 0)
                            PlayerInventory.inventory.Remove(entry.Key);
                    }
                    else
                        PlayerInventory.inventory.Remove(entry.Key);

                    _playerInventory.SaveInventory();
                    _playerInventory.LoadInventory();
                    if (_hasAPathBlocked)
                        _pathToblock.SetActive(false);
                    break;
                }
            }
        }
        else if (!_objectToGive && _monstersToKill && !_questResolved)
        {
            for (int i = 0; i < _questEnemies.Count; i++)
            {
                if (_questEnemies[i] != null)
                {
                    return;
                }
            }
            _questGiver.dialogueLines = _dialogueEnd;
            _questResolved = true;
            if (_hasAPathBlocked)
                _pathToblock.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerInventory = collision.GetComponent<PlayerInventory>();
    }

}
