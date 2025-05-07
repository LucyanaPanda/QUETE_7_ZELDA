using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [Header("NPC Involved")]
    [SerializeField] private NPCDialogue _questGiver;

    [Header("TypeOfQuest")]
    [SerializeField] private bool _objectToGive;
    [SerializeField] private bool _monstersToKill;
    public bool resolved;

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

    [Header("Money to earn")]
    [SerializeField] private int rewardMoney;

    public void IfQuestResolved()
    {
        PlayerInventory playerInventory = PlayerInventory.Instance;
        if (_objectToGive && !_monstersToKill && !resolved)
        {
            foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
            {
                if (entry.Key == _questObject)
                {
                    resolved = true;

                    PlayerInventory.inventory[entry.Key] = entry.Value - 1;
                    if (PlayerInventory.inventory[entry.Key] <= 0)
                        PlayerInventory.inventory.Remove(entry.Key);
                    playerInventory.SaveInventory();
                    playerInventory.LoadInventory();
                    playerInventory.AddMoney(rewardMoney);

                    if (_hasAPathBlocked)
                        _pathToblock.SetActive(false);
                    break;
                }
            }
        }
        else if (!_objectToGive && _monstersToKill && !resolved)
        {
            for (int i = 0; i < _questEnemies.Count; i++)
            {
                if (_questEnemies[i] != null)
                {
                    return;
                }
            }
            resolved = true;
            if (_hasAPathBlocked)
                _pathToblock.SetActive(false);

            playerInventory.AddMoney(rewardMoney);

            //story.variablesState[variableName] = variableValue;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _playerInventory = collision.GetComponent<PlayerInventory>();
    }

}
