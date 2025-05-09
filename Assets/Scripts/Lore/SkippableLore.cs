using Ink.Runtime;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkippableLore : MonoBehaviour
{
    public static SkippableLore Instance;
    public TextAsset lore;
    public string keyLore;
    public GameObject IntroductionPanel;
    [SerializeField] private TMP_Text textBox;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button nextButton;

    private Story loreStory;
    private bool done;
    public bool ending;

    [Header("FadeInOut")]
    [SerializeField] private float fadeSpeed = 0.05f;

    private float maxTimeParagraph = 10f;
    private float timer = 0f;

    private void Awake()
    {
        if (Instance != null) { Destroy(this); }
        else { Instance = this; }

        skipButton.onClick.AddListener(() => SkipIntroduction());
        nextButton.onClick.AddListener(() => NextText());

        IntroductionPanel.SetActive(false);
        done = true;
    }

    private void OnEnable()
    {
        if (done)
        {
            loreStory = new Story(lore.text);
            NextText();
        }
    }

    private void Update()
    {
        if (timer < maxTimeParagraph) { timer += Time.deltaTime; }
        else
        {
            NextText();
            timer = 0;
        }
    }

    private void NextText()
    {
        if (loreStory.canContinue) 
        {
            timer = 0;
            StartCoroutine(FadeInOut());
        }
        else
        {
            StartCoroutine(FadeInToMenu());
        }
    }

    private void SkipIntroduction()
    {
        StartCoroutine(FadeInToMenu());
    }

    IEnumerator FadeInOut()
    {
        Color currentColor = textBox.color;
        while (currentColor.a >= 0)
        {
            currentColor = currentColor - new Color(currentColor.r, currentColor.b, currentColor.g, currentColor.a - fadeSpeed);
            textBox.color -= currentColor;
            currentColor = textBox.color;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        if (!loreStory.canContinue)
        {
            IntroductionPanel.SetActive(false);
        }
        textBox.text = loreStory.Continue();
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeInToMenu()
    {
        Color currentColor = textBox.color;
        while (currentColor.a >= 0)
        {
            currentColor = currentColor - new Color(currentColor.r, currentColor.b, currentColor.g, currentColor.a - fadeSpeed);
            textBox.color -= currentColor;
            currentColor = textBox.color;
            yield return new WaitForSecondsRealtime(0.02f);
        }
        IntroductionPanel.SetActive(false);
        GlobalsVariables.Instance.SetVariable(keyLore, true);
        if (ending) { SceneManager.LoadScene(0); }
    }

    IEnumerator FadeOut()
    {
        Color currentColor = textBox.color;
        while (currentColor.a <= 1f)
        {
            currentColor = currentColor + new Color(currentColor.r, currentColor.b, currentColor.g, currentColor.a + fadeSpeed);
            textBox.color = currentColor;
            currentColor = textBox.color;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
