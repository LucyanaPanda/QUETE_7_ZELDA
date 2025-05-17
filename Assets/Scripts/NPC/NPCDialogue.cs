using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class NPCDialogue : MonoBehaviour
{
    [Header("NPC has quest")]
    [SerializeField] private Quest _quest;
    [SerializeField] private bool _hasQuest;

    [Header("NPC is a merchand")]
    [SerializeField] private bool _isAMerchand;
    [SerializeField] private NPCShop _shop;

    [SerializeField] private Creature _npcData;

    [SerializeField] private GlobalsVariables _variables;

    public TextAsset inkFile;
    public bool checkGlobalsVariables = false;
    public DialogueManager dialogueManager;

    private float timer = 0f;

    private void Update()
    {
        if (timer >= 1f && !checkGlobalsVariables)
        {
            if (_hasQuest && _npcData.questCompletedName != "" && _variables.GetVariable(_npcData.questCompletedName).ToString() == "1")
            {
                _quest.OnCompletedQuest();
            }
            checkGlobalsVariables = true;
            enabled = false;
        } else
        {
            timer += Time.deltaTime;
        }
    }


    private void OnEnable()
    {
        if (!checkGlobalsVariables) { return; }
        if (_hasQuest && !_isAMerchand)
        {
            _quest.IfQuestResolved(_npcData);
            dialogueManager.StartDialogue(inkFile, _npcData, this);
        }
        else if (!_hasQuest && _isAMerchand)
        {
            dialogueManager.shop = _shop;
            dialogueManager.isMerchandStory = true;
            dialogueManager.StartDialogue(inkFile, _npcData, this);
        }
        else
        {
            dialogueManager.StartDialogue(inkFile, _npcData, this);
        }
    }
}
