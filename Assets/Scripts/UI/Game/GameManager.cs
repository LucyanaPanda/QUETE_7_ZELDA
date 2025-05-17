using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("PlayerInGame")]
    public bool playerInGame;

    [Header("Lore")]
    public TextAsset loreBeginning;
    public string keyLoreBeginning;

    [Header("When we get the orb")]
    public TextAsset loreOrb;
    public string keyLoreOrb;

    [Header("EndGame")]
    public TextAsset loreEnding;
    public string keyLoreEnding;

    [Header("SkippableLore")]
    [SerializeField] private SkippableLore _skippableLore;

    private GlobalsVariables _globalsVariables;
    private AudioManager _audioManager;


    void Start()
    {
        _audioManager = AudioManager.Instance;
        _audioManager.PlaySound(AudioManager.AudioType.GameMusic);
        _audioManager.PlaySound(AudioManager.AudioType.GameAmbiant);

        _globalsVariables = GetComponent<GlobalsVariables>();
        StartLore(loreBeginning, keyLoreBeginning);
    }

    public void StartLore(TextAsset lore, string loreKey)
    {
        if (_globalsVariables.GetVariable(loreKey).ToString() == "0")
        {
            _skippableLore.lore = lore;
            _skippableLore.keyLore = loreKey;
            _skippableLore.IntroductionPanel.SetActive(true);
            return;
        }
    }
}
