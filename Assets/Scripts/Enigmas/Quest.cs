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

    [Header("Object to bring back")]
    [SerializeField] private Item _questObject;
    [SerializeField] private int _quantity;

    [Header("Monsters To Kill")]
    [SerializeField] private List<GameObject> _questEnemies;

    [Header("Path To Block")]
    [SerializeField] private List<GameObject> _pathToblock;
    [SerializeField] private bool _hasAPathBlocked;

    [Header("Money to earn")]
    [SerializeField] private int rewardMoney;

    [Header("Object Rewards")]
    [SerializeField] private List<ItemScript> rewardItems;
    [SerializeField] private List<int> rewardQuantities;

    public GlobalsVariables _variables;
    public PlayerInventory playerInventory;

    public void IfQuestResolved(Creature npcData)
    {
        if (_objectToGive && !_monstersToKill && !resolved && _variables.GetVariable(npcData.questCompletedName).ToString() == "0")
        {
            foreach (KeyValuePair<Item, int> entry in playerInventory.inventory)
            {
                if (entry.Key == _questObject)
                {
                    Debug.Log(entry.Value + " " + _quantity);
                    if (entry.Value < _quantity) { Debug.Log(entry.Value + " " + _quantity);  return; }
                    resolved = true;

                    playerInventory.inventory[entry.Key] = entry.Value - _quantity;
                    if (playerInventory.inventory[entry.Key] <= 0)
                        playerInventory.inventory.Remove(entry.Key);

                    playerInventory.saveInventory.SaveTheInventory();
                    playerInventory.saveInventory.LoadInventory();
                    playerInventory.AddMoney(rewardMoney);
                    OnCompletedQuest();

                    _variables.SetVariable(npcData.questCompletedName, 1);
                    SaveGlobalsVariables.Instance.SaveGlobalsData();
                    break;
                }
            }
        }
        else if (!_objectToGive && _monstersToKill && !resolved && _variables.GetVariable(npcData.questCompletedName).ToString() == "0")
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

            _variables.SetVariable(npcData.questCompletedName, 1);
            SaveGlobalsVariables.Instance.SaveGlobalsData();
        }
    }

    public void OnCompletedQuest()
    {
        resolved = true;

        if (_hasAPathBlocked)
        {
            foreach( GameObject blocked in _pathToblock )
            {
                blocked.SetActive(false);
            }
        }

        if (rewardItems != null && rewardItems.Count > 0)
        {
            for (int i = 0; i < rewardItems.Count; i++)
            {
                ItemScript item = rewardItems[i];
                int quantity = rewardQuantities[i];

                if (item != null && quantity > 0)
                {
                    playerInventory.AddToInventory(item, quantity);
                }
            }

            playerInventory.saveInventory.SaveTheInventory();
            playerInventory.saveInventory.LoadInventory();
        }

    }
}
