using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BeginGame : MonoBehaviour
{

    [SerializeField] private GameObject _blackScreen;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private List<string> _lines;

    public const string playerKeyBeginGame = "BeginGame";
    private int _currentIndex = 0;

    public void Start()
    {
        if (PlayerPrefs.HasKey(playerKeyBeginGame)) {
            if (PlayerPrefs.GetInt(playerKeyBeginGame) == 0)
            {
                _blackScreen.SetActive(true);
                OnBeginMonologue();
            }
        }
        else
        {
            _blackScreen.SetActive(true);
            OnBeginMonologue();
        }
    }

    public void OnBeginGame(InputAction.CallbackContext context)
    {
        if (context.started)
            OnBeginMonologue();
    }

   
    private void DisplayMonologueLine()
    {
        _text.text = _lines[_currentIndex];
    }
    public void OnBeginMonologue()
    {
        if (_currentIndex < _lines.Count)
            DisplayMonologueLine();
        else
        {
            _blackScreen.SetActive(false);
            _text.text = "";
            PlayerPrefs.SetInt(playerKeyBeginGame, 1);
            PlayerPrefs.Save();
            Destroy(this);
        }
        _currentIndex += 1;
    }
}
