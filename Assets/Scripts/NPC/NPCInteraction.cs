using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private GameObject _dialoguePanel;
    private NPCDialogue _dialogueScript;
    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _dialogueScript = GetComponent<NPCDialogue>();
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
        }
    }

    public void Interact()
    {
        if (!_dialoguePanel.activeInHierarchy) 
        { 
            _dialoguePanel.SetActive(true);
            _dialogueScript.enabled = true;
            _interactionPanel.SetActive(false);
        } 
        else
        {
            _dialogueScript.Boom();
        }
    }
}
