using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class DisplaySettings : MonoBehaviour
{
    Resolution[] _resolution;
    public Dropdown _resolutionDropdown;
    private int currentResolution;

    private void Start()
    {
        _resolution = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray() ;
        _resolutionDropdown.ClearOptions();

        DisplayOptions();
        Screen.fullScreen = true;
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolution[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void DisplayOptions()
    {
        List<string> options = new List<string>();
        for (int i = 0; i < _resolution.Length; i++)
        {
            string option = _resolution[i].width + "x" + _resolution[i].height;
            options.Add(option);

            if (_resolution[i].width == Screen.width && _resolution[i].height == Screen.height)
            {
                currentResolution = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolution;
        _resolutionDropdown.RefreshShownValue();
    }
}
