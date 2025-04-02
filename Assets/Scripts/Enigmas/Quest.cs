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

    public bool IfQuestResolved()
    {
        if (_objectToGive && !_monstersToKill && !_questResolved)
        {
            foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
            {
                if (entry.Key == _questObject)
                {
                    _questGiver.dialogueLines = _dialogueEnd;
                    _questResolved = true;
                    PlayerInventory.inventory.Remove(entry.Key);
                    _playerInventory.DisplayInventory();
                    _playerInventory.SaveInventory();
                    _playerInventory.LoadInventory();
                    if (_hasAPathBlocked)
                        _pathToblock.SetActive(false);
                    return true;
                }
            }
            return false;
        }
        else if (!_objectToGive && _monstersToKill && !_questResolved)
        {
            for (int i = 0; i < _questEnemies.Count; i++)
            {
                if (_questEnemies[i] != null)
                {
                    return false;
                }
            }
            _questGiver.dialogueLines = _dialogueEnd;
            _questResolved = true;
            if (_hasAPathBlocked)
                _pathToblock.SetActive(false);
            return true;
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerInventory = collision.GetComponent<PlayerInventory>();
    }

}
