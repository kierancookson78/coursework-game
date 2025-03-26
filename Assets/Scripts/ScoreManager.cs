using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Singleton instance

    public TextMeshProUGUI scoreText;
    private int currentScore = 0;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
            // Optionally, prevent the object from being destroyed when loading new scenes:
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.LogWarning("ScoreManager already exists! Destroying duplicate.");
            Destroy(gameObject);
            return; // Important: Exit to prevent further execution
        }

        UpdateScoreText(); // Initialize the text at start.
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + currentScore.ToString();
        }
        else
        {
            Debug.LogError("Score Text is not assigned in the Inspector!");
        }
    }

    public void EnemyKilled()
    {
        AddScore(100);
    }

    public int GetScore()
    {
        return currentScore;
    }
}
