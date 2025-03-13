using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private Image _profilImage;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _dialogueBox;
    [SerializeField] private List<string> _dialogueLines;

    private int _currentIndexLine;

    private void OnEnable()
    {
        ResetDialogue();
    }

    public void OnTextChanged(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (_currentIndexLine < _dialogueLines.Count)
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
    }

    private void DisplayDialogueLine()
    {
        _dialogueBox.text = _dialogueLines[_currentIndexLine];
    }

    private void ResetDialogue()
    {
        _currentIndexLine = 0;
    }
}
