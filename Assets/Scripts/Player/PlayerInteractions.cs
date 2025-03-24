using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] GameObject _interactionCanvas;
    IInteractable _interactable = null;

    public void TryInteract()
    {
        if(_interactable != null)
            _interactable.Interact();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_interactable == null)
            _interactable = collision.GetComponent<IInteractable>();
        else if (_interactable != null)
        {
            _interactionCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_interactable != null)
        {
            _interactable = null;
            _interactionCanvas.SetActive(false);
        }
    }
}
