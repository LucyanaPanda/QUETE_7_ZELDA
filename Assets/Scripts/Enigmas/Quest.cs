using Ink.Parsed;
using System.Collections.Generic;
using System.Xml.Linq;
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

    [Header("Path To Block")]
    [SerializeField] private GameObject _pathToblock;
    [SerializeField] private bool _hasAPathBlocked;

    [Header("Money to earn")]
    [SerializeField] private int rewardMoney;

    private GlobalsVariables _variables;
    private PlayerInventory playerInventory;

    private void Start()
    {
        _variables = GlobalsVariables.Instance;
        playerInventory = PlayerInventory.Instance;
    }

    public void IfQuestResolved(Creature npcData)
    {
        if (_objectToGive && !_monstersToKill && !resolved && _variables.GetVariable(npcData.questCompletedName).ToString() == "false")
        {
            foreach (KeyValuePair<Item, int> entry in playerInventory.inventory)
            {
                if (entry.Key == _questObject)
                {
                    resolved = true;

                    playerInventory.inventory[entry.Key] = entry.Value - 1;
                    if (playerInventory.inventory[entry.Key] <= 0)
                        playerInventory.inventory.Remove(entry.Key);
                    playerInventory.saveInventory.SaveTheInventory();
                    playerInventory.saveInventory.LoadInventory();
                    playerInventory.AddMoney(rewardMoney);
                    OnCompletedQuest();

                    _variables.SetVariable(npcData.questCompletedName, true);
                    SaveGlobalsVariables.Instance.SaveGlobalsData();
                    break;
                }
            }
        }
        else if (!_objectToGive && _monstersToKill && !resolved && !(bool)_variables.GetVariable(npcData.questCompletedName))
        {
            for (int i = 0; i < _questEnemies.Count; i++)
            {
                if (_questEnemies[i] != null)
                {
                    return;
                }
            }

            OnCompletedQuest();
            playerInventory.AddMoney(rewardMoney);

            _variables.SetVariable(npcData.questCompletedName, true);
            SaveGlobalsVariables.Instance.SaveGlobalsData();

        }
    }

    public void OnCompletedQuest()
    {
        resolved = true;
        if (_hasAPathBlocked)
            _pathToblock.SetActive(false);
    }
}
