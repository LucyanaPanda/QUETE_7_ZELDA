using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    [Header("NPC has quest")]
    [SerializeField] private Quest _quest;
    [SerializeField] private bool _hasQuest;
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private Image _profilImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _dialogueBox;
    public List<string> dialogueLines;

    private int _currentIndexLine;

    private void OnEnable()
    {
        ResetDialogue();
        if (_hasQuest)
            _quest.IfQuestResolved();
        Boom();
    }

    public void OnTextChanged(InputAction.CallbackContext context)
    {
        //if (context.started)
        //{
        //    if (_currentIndexLine < dialogueLines.Count)
        //    {
        //        DisplayDialogueLine();
        //    }
        //    else
        //    {
        //        ResetDialogue();
        //        _dialoguePanel.SetActive(false);
        //        this.enabled = false;
        //    }
        //    _currentIndexLine += 1;
        //}
    }

    public void Boom()
    {
        if (_currentIndexLine < dialogueLines.Count)
        {
            DisplayDialogueLine();
        }
        else
        {
            ResetDialogue();
            _dialoguePanel.SetActive(false);
            this.enabled = false;
        }
        _currentIndexLine += 1;
    }

    private void DisplayDialogueLine()
    {
        _dialogueBox.text = dialogueLines[_currentIndexLine];
    }

    private void ResetDialogue()
    {
        _currentIndexLine = 0;
    }
}
