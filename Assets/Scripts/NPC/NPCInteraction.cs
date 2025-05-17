using System;
using UnityEngine;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    [Header("DialogueManager")]
    [SerializeField] private DialogueManager _dialogueManager;

    [Header("UI Panel")]
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private GameObject _dialoguePanel;
    
    private NPCDialogue _dialogueScript;

    private void Start()
    {
        _dialogueScript = GetComponent<NPCDialogue>();
        _dialogueScript.dialogueManager = _dialogueManager;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _interactionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        { 
            _interactionPanel.SetActive(false);
            _dialoguePanel.SetActive(false);
            _dialogueScript.enabled = false;
            _dialogueManager.dialoguePlayed = false;
        }
    }

    public void Interact()
    {
        if (!_dialoguePanel.activeInHierarchy) 
        { 
            _dialoguePanel.SetActive(true);
            _dialogueScript.enabled = true;
            _interactionPanel.SetActive(false);
            _dialogueManager.ResetChoices();
        } 
        else if (_dialoguePanel.activeInHierarchy && !_dialogueManager.hasChoices) 
        {
            _dialogueManager.NextLine();
        }
    }
}
