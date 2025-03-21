using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private MenuButton _settingsButton;
    [SerializeField] private MenuButton _quitSettings;

    [Header("Audio")]
    [SerializeField] private GameObject _audioPanel;
    [SerializeField] private MenuButton _audioButton;

    [Header("Display")]
    [SerializeField] private GameObject _displayPanel;
    [SerializeField] private MenuButton _displayButton;

    private void Start()
    {
        //Settings;
        _settingsButton.OnButtonPressed.AddListener(() => ShowHideSettings(true));
        _quitSettings.OnButtonPressed.AddListener(() => ShowHideSettings(false));

        //Audio
        _audioButton.OnButtonPressed.AddListener(() => ShowHideAudio(true));
        //Display
        _displayButton.OnButtonPressed.AddListener(() => ShowHideDisplay(true));
    }

    public void ShowHideSettings(bool settings)
    {
        _settingsPanel.SetActive(settings);
    }

    public void ShowHideAudio(bool audio)
    {
        _audioPanel.SetActive(audio);
        _displayPanel.SetActive(!audio);
    }
    public void ShowHideDisplay(bool display)
    {
        _displayPanel.SetActive(display);
        _audioPanel.SetActive(!display);
    }
}
