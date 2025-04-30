using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public void OnBackButtonClick()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
