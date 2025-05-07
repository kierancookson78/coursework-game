using UnityEngine;
using UnityEngine.SceneManagement;

// Button to load the launch menu.
public class BackToMenu : MonoBehaviour
{
    public void OnBackButtonClick()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }
}
