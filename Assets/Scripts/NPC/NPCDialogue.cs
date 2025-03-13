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
    [SerializeField] private TMP_Text _dialogueBlock;
    [SerializeField] private List<string> _dialogueLines;

    private int _currentIndexLine;

    private void OnEnable()
    {
        ResetDialogue();
        DisplayDialogueLine();
    }

    public void OnTextChanged(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            _currentIndexLine += 1;
            if (_currentIndexLine < _dialogueLines.Count)
            {
                DisplayDialogueLine();
            }
            else
            {
                ResetDialogue();
                gameObject.SetActive(false);
            }

        }
    }

    private void DisplayDialogueLine()
    {
        _dialogueBlock.text = _dialogueLines[_currentIndexLine];
    }

    private void ResetDialogue()
    {
        _currentIndexLine = 0;
    }
}
