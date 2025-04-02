using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.EventSystems.EventTrigger;

public class EndGame : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerInventory _inventory;
    [SerializeField] private Item _questObject;

    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private TMP_Text _text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _inventory = collision.GetComponent<PlayerInventory>();
    }

    public void Interact()
    {
        foreach (KeyValuePair<Item, int> entry in PlayerInventory.inventory)
        {
            if (entry.Key == _questObject)
            {
                PlayerInventory.inventory.Remove(entry.Key);
                _inventory.DisplayInventory();
                _inventory.SaveInventory();
                _inventory.LoadInventory();
                OnEndOfGame();
                Destroy(this);
            }
        }
    }

     private void OnEndOfGame()
    {
        _blackScreen.SetActive(true);
        _text.text = "Fin du jeu";
        float timer = 0;
        while (timer > 3f)
        {
            timer += Time.deltaTime;
        }
        SceneManager.LoadScene(0);
        PlayerPrefs.DeleteAll();
    }
}
