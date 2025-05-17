using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue elements")]
    public NPCDialogue currentNPC;
    public bool dialoguePlayed = false;
    public bool hasChoices = false;

    [Header("MerchandStory")]
    public bool isMerchandStory;
    public NPCShop shop;

    [Header("UI elements for dialogue")]
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private Image _profilImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _dialogueBox;
    [SerializeField] private GameObject choicesParent;
    [SerializeField] private GameObject choicePrefab;

    private GlobalsVariables _variables;

    private Story currentStory;

    private void Start()
    {
        _variables = GetComponent<GlobalsVariables>();
    }

    public void StartDialogue( TextAsset inkFile, Creature npcData, NPCDialogue npc)
    {
        currentStory = new Story(inkFile.text);
        _variables.BindToStory(currentStory);
        _variables.StartListeningStory(currentStory);
        currentNPC = npc;
        ResetDialogue();
        NextLine();
        _nameText.text = npcData.nameCreature;
        _profilImage.sprite = npcData.image;
        dialoguePlayed = true;
    }

    public void NextLine()
    {
        if (currentStory.canContinue)
        {
            DisplayDialogueLine();
        }
        else
        {
            ResetDialogue();
            _dialoguePanel.SetActive(false);
            currentNPC.enabled = false;
            dialoguePlayed = false;
            _variables.StopListeningStroy(currentStory);
        }
    }

    private void DisplayDialogueLine()
    {
        _dialogueBox.text = currentStory.Continue();
        DisplayChoices();
    }

    private void ResetDialogue()
    {
        _dialogueBox.text = "";
    }

    private void DisplayChoices()
    {
        List<Choice> choices = currentStory.currentChoices;
        for (int i = 0; i < choices.Count; i++)
        {
            int indexChoice = i;
            GameObject choice = Instantiate(choicePrefab, choicesParent.transform, false);
            choice.GetComponent<Button>().onClick.AddListener(() => MakeChoice(indexChoice));
            choice.GetComponent<Button>().onClick.AddListener(() => ResetChoices());
            choice.GetComponentInChildren<TMP_Text>().text = choices[i].text;
            hasChoices = true;
        }
    }

    public void ResetChoices()
    {
        foreach (Transform child in choicesParent.transform)
        {
            Destroy(child.gameObject);
        }
        hasChoices = false;
    }

    private void MakeChoice(int index)
    {
        currentStory.ChooseChoiceIndex(index);
        NextLine();
        if (isMerchandStory && index == 0) { shop.enabled = true; isMerchandStory = false; NextLine(); }
    }
}
