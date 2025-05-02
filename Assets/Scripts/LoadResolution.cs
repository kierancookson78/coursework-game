using UnityEngine;

public class LoadResolution : MonoBehaviour
{
    private Resolution[] resolutions;
    void Start()
    {
        resolutions = Screen.resolutions;
        ApplyResolution();
    }

    private void ApplyResolution()
    {
        // Load the saved resolution index
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", -1);

        if (savedResolutionIndex != -1 && savedResolutionIndex < resolutions.Length)
        {
            // Apply the saved resolution
            Resolution savedResolution = resolutions[savedResolutionIndex];
            Screen.SetResolution(savedResolution.width, savedResolution.height, FullScreenMode.ExclusiveFullScreen, savedResolution.refreshRateRatio);
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
            PlayerPrefs.SetInt("ResolutionIndex", currentResolutionIndex);
            PlayerPrefs.Save();
        }
    }
}
