using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumnSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _sfxSlider;
    [SerializeField] private Slider _ambiantSlider;

    float volumn;

    private void Awake()
    {
        LoadVolumn();
    }

    public void SetMusicVolumn()
    {
        volumn = _musicSlider.value;
        _mixer.SetFloat("Music", Mathf.Log10(volumn)*20);
        PlayerPrefs.SetFloat("musicVolumn", volumn);
    }
    public void SetSFXVolumn()
    {
        volumn = _sfxSlider.value;
        _mixer.SetFloat("SFX", Mathf.Log10(volumn) * 20);
        PlayerPrefs.SetFloat("SFXVolumn", volumn);
    }
    public void SetAmbiantVolumn()
    {
        volumn = _ambiantSlider.value;
        _mixer.SetFloat("Ambiant", Mathf.Log10(volumn) * 20);
        PlayerPrefs.SetFloat("ambiantVolumn", volumn);
    }

    private void LoadVolumn()
    {
        if (PlayerPrefs.HasKey("musicVolumn")) 
            _musicSlider.value = PlayerPrefs.GetFloat("musicVolumn");
        else
            SetMusicVolumn();

        if (PlayerPrefs.HasKey("SFXVolumn"))
            _sfxSlider.value = PlayerPrefs.GetFloat("SFXVolumn");
        else
            SetSFXVolumn();

        if (PlayerPrefs.HasKey("ambiantVolumn"))
            _ambiantSlider.value = PlayerPrefs.GetFloat("ambiantVolumn");
        else
            SetAmbiantVolumn();
    }

    public static void LoadVolumnInGame(AudioMixer mixer)
    {
        //Music
        if (PlayerPrefs.HasKey("musicVolumn"))
            mixer.SetFloat("Music", Mathf.Log10(PlayerPrefs.GetFloat("musicVolumn")) * 20);
        else
            mixer.SetFloat("Music", Mathf.Log10(1) * 20);

        //SFX
        if (PlayerPrefs.HasKey("SFXVolumn"))
            mixer.SetFloat("SFX", Mathf.Log10(PlayerPrefs.GetFloat("SFXVolumn")) * 20);
        else
            mixer.SetFloat("SFX", Mathf.Log10(1) * 20);

        //Ambiant
        if (PlayerPrefs.HasKey("ambiantVolumn"))
            mixer.SetFloat("Ambiant", Mathf.Log10(PlayerPrefs.GetFloat("ambiantVolumn")) * 20);
        else
            mixer.SetFloat("Ambiant", Mathf.Log10(1) * 20);
    }
}


