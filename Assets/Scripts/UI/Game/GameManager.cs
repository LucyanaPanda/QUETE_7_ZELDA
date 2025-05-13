using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("PlayerInGame")]
    public bool playerInGame;

    [Header("Lore")]
    public TextAsset loreBeginning;
    public string keyLoreBeginning;

    private AudioManager _audioManager;

    private void Awake()
    {
        if (Instance != null ) { Destroy(this); }
        else { Instance = this; }
    }

    void Start()
    {
        _audioManager = AudioManager.Instance;
        _audioManager.PlaySound(AudioManager.AudioType.GameMusic);
        _audioManager.PlaySound(AudioManager.AudioType.GameAmbiant);

        if (GlobalsVariables.Instance.GetVariable(keyLoreBeginning).ToString() == "False") 
        {
            SkippableLore.Instance.lore = loreBeginning;
            SkippableLore.Instance.keyLore = keyLoreBeginning;
            SkippableLore.Instance.IntroductionPanel.SetActive(true);
            return; 
        }
    }
}
