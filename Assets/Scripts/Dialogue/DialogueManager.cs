using Ink.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public NPCDialogue currentNPC;
    public bool dialoguePlayed = false;

    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private Image _profilImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _dialogueBox;
    [SerializeField] private GameObject choicesParent;
    [SerializeField] private GameObject choicePrefab;

    private Story currentStory;

    private void Awake()
    {
        if (Instance != null) { Destroy(Instance); }
        Instance = this;
    }

    public void StartDialogue( TextAsset inkFile, Creature npcData, NPCDialogue npc)
    {
         currentStory = new Story(inkFile.text);
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
            choice.GetComponent<Button>().onClick.AddListener(() => ResetChoices(choicesParent));
            choice.GetComponentInChildren<TMP_Text>().text = choices[i].text;
        }
    }

    private void ResetChoices(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void MakeChoice(int index)
    {
        currentStory.ChooseChoiceIndex(index);
        NextLine();
    }
}
