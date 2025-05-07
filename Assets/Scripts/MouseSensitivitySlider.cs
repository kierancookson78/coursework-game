using UnityEngine;
using UnityEngine.UI;
using TMPro; // Required for TextMeshPro

public class MouseSensitivitySlider : MonoBehaviour
{
    // Public variable for the slider.  Drag and drop your slider here in the inspector.
    [SerializeField] private Slider sensitivitySlider;
    // Public variable for the TextMeshPro text. Drag and drop your TextMeshPro text object here.
    [SerializeField] private TextMeshProUGUI sensitivityValueText;

    // The name of the key used to save the sensitivity in PlayerPrefs.
    private const string SENSITIVITY_PREF_KEY = "MouseSensitivity";

    // Default sensitivity value.  This is used if no saved value exists.
    private const float DEFAULT_SENSITIVITY = 1f;

    // Minimum and maximum sensitivity values.  These are set in the Unity Inspector on the Slider.
    private float minSensitivity = 0.1f;
    private float maxSensitivity = 100f;

    // Static variable to hold the actual mouse sensitivity value.  Other scripts can access this.
    private float sensitivity;

    void Awake()
    {
        // Load the sensitivity
        LoadSensitivity(); // Load Sensitivity here

        // Ensure the slider and text components are assigned.  Important for preventing errors.
        if (sensitivitySlider == null)
        {
            return; // Stop the rest of the Awake function.
        }
        if (sensitivityValueText == null)
        {
            return; // Stop the rest of the Awake function.
        }

        // Get the min and max values from the slider.
        minSensitivity = sensitivitySlider.minValue;
        maxSensitivity = sensitivitySlider.maxValue;

        // Make sure the loaded sensitivity is within the slider's range.  This is a safety check.
        sensitivity = Mathf.Clamp(sensitivity, minSensitivity, maxSensitivity);

        // Set the slider's value to the loaded sensitivity.
        sensitivitySlider.value = sensitivity;

        // Update the sensitivity text to display the initial value.
        UpdateSensitivityText(sensitivity);

        // Add a listener to the slider's value change event.
        // This function will be called whenever the user moves the slider.
        sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
    }

    // This function is called whenever the slider's value changes.
    private void OnSensitivityChanged(float newValue)
    {
        // Update the static sensitivity variable.
        sensitivity = newValue;

        // Update the PlayerPrefs with the new value.
        PlayerPrefs.SetFloat(SENSITIVITY_PREF_KEY, newValue);
        PlayerPrefs.Save(); // Important:  Save the changes to disk!

        // Update the text display to show the new value.
        UpdateSensitivityText(newValue);
    }

    private void UpdateSensitivityText(float sensitivityValue)
    {
        // Display the sensitivity value, formatted to one decimal place.
        if (sensitivityValueText != null) // Check if the text object is still valid.
        {
            sensitivityValueText.text = sensitivityValue.ToString("F1");
        }
    }

    // Public method to get the current sensitivity.  Other scripts can call this.
    public float GetSensitivity()
    {
        return sensitivity;
    }

    // Public method to load the sensitivity.
    public void LoadSensitivity()
    {
        sensitivity = PlayerPrefs.GetFloat(SENSITIVITY_PREF_KEY, DEFAULT_SENSITIVITY);
    }
}

