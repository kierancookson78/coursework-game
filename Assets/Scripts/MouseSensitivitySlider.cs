using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MouseSensitivitySlider : MonoBehaviour
{
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityValueText;

    private const string SENSITIVITY_PREF_KEY = "MouseSensitivity";
    private const float DEFAULT_SENSITIVITY = 1f;
    private float sensitivity;

    void Awake()
    {
        LoadSensitivity();

        if (sensitivitySlider == null)
        {
            return;
        }

        if (sensitivityValueText == null)
        {
            return; 
        }

        // Set the slider's value to the loaded sensitivity.
        sensitivitySlider.value = sensitivity;

        // Update the sensitivity text to display the initial value.
        UpdateSensitivityText(sensitivity);
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    private void OnSensitivityChanged(float newValue)
    {
        sensitivity = newValue;

        // Update the PlayerPrefs with the new value.
        PlayerPrefs.SetFloat(SENSITIVITY_PREF_KEY, newValue);
        PlayerPrefs.Save();

        // Update the text display to show the new value.
        UpdateSensitivityText(newValue);
    }

    private void UpdateSensitivityText(float sensitivityValue)
    {
        if (sensitivityValueText != null)
        {
            sensitivityValueText.text = sensitivityValue.ToString("F1");
        }
    }

    public float GetSensitivity()
    {
        return sensitivity;
    }

    public void LoadSensitivity()
    {
        sensitivity = PlayerPrefs.GetFloat(SENSITIVITY_PREF_KEY, DEFAULT_SENSITIVITY);
    }
}

