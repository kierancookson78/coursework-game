using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScreen : MonoBehaviour
{
    public void OnPlayButtonClick()
    {
        SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
    }
}
