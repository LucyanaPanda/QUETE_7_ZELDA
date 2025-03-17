using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Credits")]
    [SerializeField] private GameObject _credits;
    [SerializeField] private MenuButton _creditsButton;
    [SerializeField] private MenuButton _quitCredits;


    private void Start()
    {
        _creditsButton.OnButtonPressed.AddListener(() => ShowHideCredits(true));
        _quitCredits.OnButtonPressed.AddListener(() => ShowHideCredits(false));
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
