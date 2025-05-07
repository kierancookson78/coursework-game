using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ResolutionSelector : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<string> resolutionOptions = new List<string>();
    private int selectedResolutionIndex;

    private void Start()
    {
        // Get available resolutions
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // Populate the dropdown with resolution options
        foreach (Resolution res in resolutions)
        {
            resolutionOptions.Add(res.width + " x " + res.height + " @ " + res.refreshRateRatio + "Hz");
        }
        resolutionDropdown.AddOptions(resolutionOptions);

        // Load saved resolution
        LoadResolution();

        resolutionDropdown.onValueChanged.AddListener(OnResolutionChange);
    }

    private void OnResolutionChange(int index)
    {
        // Store the selected index, but don't apply the resolution yet
        selectedResolutionIndex = index;
    }

    public void SaveResolution()
    {
        // Set the selected resolution
        Resolution selectedResolution = resolutions[selectedResolutionIndex];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, FullScreenMode.ExclusiveFullScreen, selectedResolution.refreshRateRatio);

        // Save the selected resolution index
        PlayerPrefs.SetInt("ResolutionIndex", selectedResolutionIndex);
        PlayerPrefs.Save();
    }

    private void LoadResolution()
    {
        // Load the saved resolution index
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", -1);

        if (savedResolutionIndex != -1)
        {
            // Apply the saved resolution
            Resolution savedResolution = resolutions[savedResolutionIndex];
            Screen.SetResolution(savedResolution.width, savedResolution.height, FullScreenMode.ExclusiveFullScreen, savedResolution.refreshRateRatio);
            resolutionDropdown.value = savedResolutionIndex;
            selectedResolutionIndex = savedResolutionIndex;
        }
        else
        {
            //If no resolution is saved, set to the current resolution.
            int currentResolutionIndex = 0;
            for (int i = 0; i < resolutions.Length; i++)
            {
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = i;
                    break;
                }
            }
            Resolution defaultResolution = resolutions[currentResolutionIndex];
            Screen.SetResolution(defaultResolution.width, defaultResolution.height, FullScreenMode.ExclusiveFullScreen, Screen.currentResolution.refreshRateRatio);
            resolutionDropdown.value = currentResolutionIndex;
            PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
            PlayerPrefs.Save();
            selectedResolutionIndex = currentResolutionIndex;
        }
    }
}

