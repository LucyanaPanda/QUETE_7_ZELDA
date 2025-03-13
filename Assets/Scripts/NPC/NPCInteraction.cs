using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class NPCInteraction : MonoBehaviour
{
    [SerializeField] private GameObject _interactionPanel;
    [SerializeField] private GameObject _dialoguePanel;
    private BoxCollider2D _collider;

    private void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
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
        }
    }

    public void InteractionStarted(InputAction.CallbackContext context)
    {
        if (context.started && _dialoguePanel.activeInHierarchy) 
            _dialoguePanel.SetActive(true);
    }
}
