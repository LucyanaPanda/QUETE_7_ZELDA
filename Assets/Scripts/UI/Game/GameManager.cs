using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioManager _audioManager;

    [Header("Lore")]
    public TextAsset loreBeginning;
    public string keyLoreBeginning;

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

    //private void OnDestroy()
    //{
    //    _audioManager.StopSound(AudioManager.AudioType.GameMusic);
    //    _audioManager.StopSound(AudioManager.AudioType.GameAmbiant);
    //}
}
