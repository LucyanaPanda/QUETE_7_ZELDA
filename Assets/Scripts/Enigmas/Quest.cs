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
    [SerializeField] private bool _talkToNPC;
    [SerializeField] private bool _talk;
    [SerializeField] private NPCDialogue target;
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

    public GlobalsVariables variables;
    public SaveGlobalsVariables saveVariables;
    public PlayerInventory playerInventory;

    public void IfQuestResolved(Creature npcData)
    {
        if (_objectToGive && !_monstersToKill && !_talkToNPC &&!_talk && !resolved && variables.GetVariable(npcData.questCompletedName).ToString() == "0")
        {
            foreach (KeyValuePair<Item, int> entry in playerInventory.inventory)
            {
                if (entry.Key == _questObject)
                {
                    if (entry.Value < _quantity) { return; }
                    resolved = true;

                    playerInventory.inventory[entry.Key] = entry.Value - _quantity;
                    if (playerInventory.inventory[entry.Key] <= 0)
                        playerInventory.inventory.Remove(entry.Key);

                    playerInventory.saveInventory.SaveTheInventory();
                    playerInventory.saveInventory.LoadInventory();
                    OnCompletedQuest();

                    GiveRewards(npcData);
                    break;
                }
            }
        }
        else if (!_objectToGive && _monstersToKill && !_talkToNPC &&!_talk && !resolved && variables.GetVariable(npcData.questCompletedName).ToString() == "0")
        {
            for (int i = 0; i < _questEnemies.Count; i++)
            {
                if (_questEnemies[i] != null)
                {
                    return;
                }
            }

            OnCompletedQuest();
            GiveRewards(npcData);
        }
        else if (!_objectToGive && !_monstersToKill && _talkToNPC &&!_talk && target != null)
        {
            if (target.talkToOnce)
            {
                OnCompletedQuest();
                GiveRewards(npcData);
            }
        }
        else if (!_objectToGive && !_monstersToKill && !_talkToNPC && _talk)
        {
            if (_questGiver.talkToOnce)
            {
                OnCompletedQuest();
                GiveRewards(npcData);
            }
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
    }

    private void GiveRewards(Creature npcData)
    {
        playerInventory.AddMoney(rewardMoney);

        variables.SetVariable(npcData.questCompletedName, 1);
        saveVariables.SaveGlobalsData();

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
