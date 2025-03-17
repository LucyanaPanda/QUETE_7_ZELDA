using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    [Header("Pause")]
    private bool _gameIsPaused;
    [SerializeField] private GameObject _pausePanel;

    [Header("Settings")]
    private bool _isSettingsOn; 
    [SerializeField] private GameObject _settingsPanel;

    [Header("Player")]
    [SerializeField] private PlayerController _player;

    public void PausedResume(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!_gameIsPaused)
            {
                _pausePanel.SetActive(true);
                PauseGame();
            }
            else
            {
                _pausePanel.SetActive(false);
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        _gameIsPaused = true;
        Time.timeScale = 0f;
        _player.enabled = false;
    }

    public void ResumeGame()
    {
        _gameIsPaused = false;
        Time.timeScale = 1f;
        _player.enabled = true;
    }

    public void ShowHideSettings()
    {
        if (_isSettingsOn) 
            _settingsPanel.SetActive(false);
        else
            _settingsPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

