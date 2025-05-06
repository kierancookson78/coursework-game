using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; } // Singleton instance

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI rankText;
    private LeaderboardService leaderboardService;
    private LevelUpAnimation levelUpAnimation;
    private PowerUpAdder powerUpAdder;
    private int currentScore = 0;
    private int shieldStreak = 0;
    private int cannonStreak = 0;
    private int nukeStreak = 0;
    private int playerRank = 0;
    private int scoreToNextRank = 0;

    private void Awake()
    {
        // Singleton pattern implementation
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("ScoreManager already exists! Destroying duplicate.");
            Destroy(gameObject);
            return;
        }

        UpdateScoreText();
    }

    void Start()
    {
        powerUpAdder = FindAnyObjectByType<PowerUpAdder>();
        leaderboardService = UserManager.Instance.GetComponent<LeaderboardService>();
        (int currentRank, int scoreToNext) = leaderboardService.GetUserLeaderboardInfo();
        playerRank = currentRank;
        scoreToNextRank = scoreToNext;
        rankText.text = "Current Rank: " + playerRank + "\n" + "Score to rank up: " + scoreToNextRank;
        levelUpAnimation = GetComponent<LevelUpAnimation>();
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
        bool isSheildUnlocked = powerUpAdder.ShieldIsUnlocked();
        bool isCannonUnlocked = powerUpAdder.CannonIsUnlocked();
        bool isNukeUnlocked = powerUpAdder.NukeIsUnlocked();

        if (!isSheildUnlocked)
        {
            shieldStreak++;
        }
        if (!isCannonUnlocked)
        {
            cannonStreak++;
        }
        if (!isNukeUnlocked)
        {
            nukeStreak++;
        }

        AddScore(100);
        leaderboardService.AddScore(currentScore);
        (int currentRank, int scoreToNext) = leaderboardService.GetUserLeaderboardInfo();
        int lastRank = playerRank;
        playerRank = currentRank;
        scoreToNextRank = scoreToNext;

        rankText.text = "Current Rank: " + playerRank + "\n" + "Score to rank up: " + scoreToNextRank;

        if (lastRank > playerRank)
        {
            Debug.Log("Ranked Up");
            RankedUp();
        }
    }

    private void RankedUp()
    {
        levelUpAnimation.PlayLevelUpAnimation();
    }

    public int GetScore()
    {
        return currentScore;
    }

    public int GetShieldStreak()
    {
        return shieldStreak;
    }

    public int GetCannonStreak()
    {
        return cannonStreak;
    }

    public int GetNukeStreak()
    {
        return nukeStreak;
    }

    public void ResetShieldStreak()
    {
        shieldStreak = 0;
    }

    public void ResetCannonStreak()
    {
        cannonStreak = 0;
    }

    public void ResetNukeStreak()
    {
        nukeStreak = 0;
    }
}
