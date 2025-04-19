using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginAndRegistration : MonoBehaviour
{
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TextMeshProUGUI messageText;

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

        if (UserManager.Instance.LoadUser(username))
        {
            if (messageText != null)
            {
                messageText.text = "User '" + username + "' loaded.";
            }
            // User loaded, proceed to game or next scene
            Debug.Log("User loaded");
            SceneManager.LoadSceneAsync(5);
        }
        else
        {
            if (UserManager.Instance.CreateUser(username))
            {
                if (messageText != null)
                {
                    messageText.text = "User '" + username + "' created.";
                }
                // User created, proceed to game or next scene
                Debug.Log("User Created");
                SceneManager.LoadSceneAsync(5);
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
