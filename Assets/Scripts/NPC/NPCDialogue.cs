using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    [Header("NPC has quest")]
    [SerializeField] private Quest _quest;
    [SerializeField] private bool _hasQuest;

    [Header("NPC is a merchand")]
    [SerializeField] private bool _isAMerchand;
    [SerializeField] private NPCShop _shop;

    [SerializeField] private Creature npcData;

    public TextAsset inkFile;

    private void OnEnable()
    {
        if (_hasQuest && !_isAMerchand)
            _quest.IfQuestResolved();

        DialogueManager.Instance.StartDialogue(inkFile, npcData, this);
    }
}
