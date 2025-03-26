using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginAndRegistration : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private UserManager userManager; // Assign your UserManager in the Inspector
    [SerializeField] private TextMeshProUGUI messageText; // Optional: For feedback messages

    void Start()
    {
        if (userManager == null)
        {
            Debug.LogError("UserManager not assigned in the Inspector!");
        }
    }

    public void OnLoginButtonClick()
    {
        string username = usernameInputField.text;

        if (string.IsNullOrEmpty(username))
        {
            if (messageText != null)
            {
                messageText.text = "Username cannot be empty.";
            }
            return;
        }

        if (userManager.LoadUser(username))
        {
            if (messageText != null)
            {
                messageText.text = "User '" + username + "' loaded.";
            }
            // User loaded, proceed to game or next scene
            Debug.Log("User loaded");
            SceneManager.LoadSceneAsync(2);
        }
        else
        {
            if (userManager.CreateUser(username))
            {
                if (messageText != null)
                {
                    messageText.text = "User '" + username + "' created.";
                }
                // User created, proceed to game or next scene
                Debug.Log("User Created");
                SceneManager.LoadSceneAsync(2);
            }
            else
            {
                if (messageText != null)
                {
                    messageText.text = "User '" + username + "' already exists.";
                }
                Debug.Log("User already exists");
            }

        }
    }
}
