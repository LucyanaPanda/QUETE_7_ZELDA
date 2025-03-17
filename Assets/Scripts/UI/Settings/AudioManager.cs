using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer _mixer;
    private void Start()
    {
        VolumnSettings.LoadVolumnInGame(_mixer);
    }
}
