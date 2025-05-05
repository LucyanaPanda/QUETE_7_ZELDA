using UnityEngine;

public class GameManager : MonoBehaviour
{
    private AudioManager _audioManager;
    void Start()
    {
        _audioManager = AudioManager.Instance;
        _audioManager.PlaySound(AudioManager.AudioType.GameMusic);
        _audioManager.PlaySound(AudioManager.AudioType.GameAmbiant);
    }

    private void OnDestroy()
    {
        _audioManager.StopSound(AudioManager.AudioType.GameMusic);
        _audioManager.StopSound(AudioManager.AudioType.GameAmbiant);
    }
}
