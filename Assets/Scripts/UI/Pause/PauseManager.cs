using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Header("Pause")]
    private bool _gameIsPaused;
    [SerializeField] private GameObject _pausePanelGameObject;
    [SerializeField] private GameObject _pausePanel;

    [Header("Player Interface")]
    [SerializeField] private GameObject _playerInterface;

    [Header("HotbarUI")]
    [SerializeField] private GameObject _hotbarUIPanel;

    [Header("Settings")]
    private bool _isSettingsOn; 
    [SerializeField] private GameObject _settingsPanel;

    [Header("Save")]
    [SerializeField] private GameObject _savePanel;

    [Header("Player")]
    [SerializeField] private PlayerController _player;

    public void PausedResume(InputAction.CallbackContext context)
    {
        if (context.started) 
            ResumePause();
    }

    public void ResumePause()
    {
        if (!_gameIsPaused)
        {
           _pausePanelGameObject.SetActive(true);
            _pausePanel.SetActive(true);
            _hotbarUIPanel.SetActive(false);
            _playerInterface.SetActive(false);
            PauseGame();
        }
        else
        {
            _pausePanelGameObject.SetActive(false);
            _hotbarUIPanel.SetActive(true);
            _playerInterface.SetActive(true);
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        _gameIsPaused = true;
        //Time.timeScale = 0f;
        _player.enabled = false;
    }

    public void ResumeGame()
    {
        _gameIsPaused = false;
        //Time.timeScale = 1f;
        _player.enabled = true;
    }

    public void ShowHideSettings()
    {
        if (_isSettingsOn) 
            _settingsPanel.SetActive(false);
        else
            _settingsPanel.SetActive(true);
    }

    public void ShowHideSave(bool save)
    {
        _savePanel.SetActive(save);
        _pausePanel.SetActive(!save);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

