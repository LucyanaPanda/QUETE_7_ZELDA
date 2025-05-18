using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;

public class EndGame : InteractableScript
{
    [SerializeField] private Item _questObject;
    [SerializeField] private SkippableLore _skippableLore;
    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private TMP_Text _text;
    public TextAsset loreEnding;
    public string keyLoreEnding;
    private Story loreEndingStory;

    [SerializeField] private bool _hasOffered = false;

    public override void Interact()
    {
        if (!_hasOffered)
        {
            foreach (KeyValuePair<Item, int> entry in _playerInventory.inventory)
            {
                if (entry.Key == _questObject)
                {
                    _playerInventory.inventory.Remove(entry.Key);
                    _playerInventory.inventory.Clear();
                    _playerInventory.saveInventory.SaveTheInventory();

                    _blackScreen.SetActive(true);
                    _hasOffered = true;

                    _skippableLore.lore = loreEnding;
                    _skippableLore.keyLore = keyLoreEnding;
                    _skippableLore.ending = true;
                    _skippableLore.IntroductionPanel.SetActive(true);
                    return;
                }
            }
        }
    }

}
