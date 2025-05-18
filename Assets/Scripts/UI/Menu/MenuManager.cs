using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Credits")]
    [SerializeField] private GameObject _credits;
    [SerializeField] private MenuButton _creditsButton;
    [SerializeField] private MenuButton _quitCredits;

    private AudioManager _audioManager;

    private void Start()
    {
        _creditsButton.OnButtonPressed.AddListener(() => ShowHideCredits(true));
        _quitCredits.OnButtonPressed.AddListener(() => ShowHideCredits(false));

        _audioManager = AudioManager.Instance;
        _audioManager.PlaySound(AudioManager.AudioType.MenuMusic);
        _audioManager.PlaySound(AudioManager.AudioType.MenuAmbiant);
        _audioManager.StopSound(AudioManager.AudioType.GameMusic);
        _audioManager.StopSound(AudioManager.AudioType.GameAmbiant);
    }

    private void OnDestroy()
    {
        _audioManager.StopSound(AudioManager.AudioType.MenuMusic);
        _audioManager.StopSound(AudioManager.AudioType.MenuAmbiant);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    //Display the Credits page and exit it
    public void ShowHideCredits(bool credits)
    {
        _credits.SetActive(credits);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
