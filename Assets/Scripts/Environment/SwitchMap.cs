using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SwitchMap : MonoBehaviour, IInteractable
{
    [Header("Maps")]
    [SerializeField] private GameObject _outside;
    [SerializeField] private GameObject _inside;
    private bool _isInside;

    [Header("Interaction UI")]
    [SerializeField] private GameObject interactionPanel;

    private void SwitchingMap()
    {
        if (_isInside)
        {
            _isInside = false;
            _outside.SetActive(true);
            _inside.SetActive(false);
        }
        else
        {
            _isInside = true;
            _outside.SetActive(false);
            _inside.SetActive(true);
        }
    }

    public void Interact()
    {
        SwitchingMap();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerManager player = collision.GetComponent<PlayerManager>();
        if (player != null)
        {
            interactionPanel.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerManager player = collision.GetComponent<PlayerManager>();
        if (player != null)
        {
            interactionPanel.SetActive(false);
        }
    }
}
