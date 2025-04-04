using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class BeginningGame : InteractableScript
{
    [SerializeField] private Item _questObject;

    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private List<string> _lines;

    [SerializeField] private bool _hasOffered = false;

    private int _currentIndex = 0;

    public void OnEndGame(InputAction.CallbackContext context)
    {
        if (_hasOffered && context.started)
            OnEndMonologue();
    }

    public override void Interact()
    {
        foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
        {
            if (entry.Key == _questObject)
            {
                PlayerInventory.inventory.Remove(entry.Key);
                PlayerInventory.inventory.Clear();
                _playerInventory.SaveInventory();

                _blackScreen.SetActive(true);
                _hasOffered=true;
                break;
            }
        }
    }
    private void DisplayMonologueLine()
    {
        _text.text = _lines[_currentIndex];
    }
    public void OnEndMonologue()
    {
        if (_currentIndex < _lines.Count)
            DisplayMonologueLine();
        else
        {
            PlayerPrefs.DeleteAll();
            SceneManager.LoadScene(0);
            Destroy(this);
        }
        _currentIndex += 1;
    }

}
