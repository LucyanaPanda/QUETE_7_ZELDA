using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject _settingsCanvas;
    [SerializeField] private MenuButton _settingsButton;
    [SerializeField] private MenuButton _quitSettings;

    [Header("Audio")]
    [SerializeField] private GameObject _audio;
    [SerializeField] private MenuButton _audioButton;

    [Header("Display")]
    [SerializeField] private GameObject _display;
    [SerializeField] private MenuButton _displayButton;

    private void Start()
    {
        _settingsButton.OnButtonPressed.AddListener(() => ShowHideSettings(true));
        _quitSettings.OnButtonPressed.AddListener(() => ShowHideSettings(false));

        _audioButton.OnButtonPressed.AddListener(() => ShowHideAudio(true));
        _displayButton.OnButtonPressed.AddListener(() => ShowHideDisplay(true));
    }

    public void ShowHideSettings(bool settings)
    {
        _settingsCanvas.SetActive(settings);
    }

    public void ShowHideAudio(bool audio)
    {
        _audio.SetActive(audio);
        _display.SetActive(!audio);
    }
    public void ShowHideDisplay(bool display)
    {
        _display.SetActive(display);
        _audio.SetActive(!display);
    }
}
