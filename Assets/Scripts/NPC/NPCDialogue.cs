using UnityEngine;

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
    private bool checkGlobalsVariables = false;

    private void Start()
    {
        if (npcData.questCompletedName != "" && GlobalsVariables.Instance.GetVariable(npcData.questCompletedName).ToString() != "false")
        {
            _quest.OnCompletedQuest();
        }
        enabled = false;
        checkGlobalsVariables = true;
    }
    private void OnEnable()
    {
        if (!checkGlobalsVariables) { return; }

        if (_hasQuest && !_isAMerchand)
        {
            _quest.IfQuestResolved(npcData);
            DialogueManager.Instance.StartDialogue(inkFile, npcData, this);
        }
        else if (!_hasQuest && _isAMerchand)
        {
            DialogueManager.Instance.shop = _shop;
            DialogueManager.Instance.isMerchandStory = true;
            DialogueManager.Instance.StartDialogue(inkFile, npcData, this);
        }
    }
}
