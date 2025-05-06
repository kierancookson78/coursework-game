using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System; // For DateTime

[System.Serializable]
public class LeaderboardData
{
    public List<LeaderboardEntry> entries;
    public DateTime lastUpdated;
}

[System.Serializable]
public class LeaderboardEntry
{
    public string username;
    public int highScore;
}

public class LeaderboardManager : MonoBehaviour, ILeaderboard
{
    private LeaderboardData leaderboardData;
    private string leaderboardSavePath;
    private const string LeaderboardFileName = "leaderboard.dat"; // Consistent file name
    private int previousUserRank = -1; // To track rank changes

    void Awake()
    {
        leaderboardSavePath = Path.Combine(Application.persistentDataPath, LeaderboardFileName);
        LoadLeaderboard(); // Load on startup
    }

    public void AddScore(int score)
    {
        if (UserManager.Instance != null)
        {
            UserManager.Instance.UpdateHighScore(score);
            Debug.Log($"High score updated for user '{UserManager.Instance.GetUsername()}' to {UserManager.Instance.GetHighScore()}.");
            UpdateLeaderboard(); // Update the cache whenever a score is added.
        }
        else
        {
            Debug.LogError("UserManager singleton instance not found. Cannot update high score.");
        }
    }

    public string GetLeaderboard()
    {
        string currentUser = UserManager.Instance?.GetUsername();
        if (leaderboardData == null)
        {
            UpdateLeaderboard();
        }

        if (leaderboardData == null || leaderboardData.entries == null || leaderboardData.entries.Count == 0)
        {
            return "Leaderboard is empty."; //handles the case where the file load was empty
        }

        string leaderboardString = "--- HIGH SCORES ---\n";
        leaderboardString += "Current User ** \n";
        int rank = 1;
        foreach (LeaderboardEntry entry in leaderboardData.entries)
        {
            if (entry.username == currentUser)
            {
                leaderboardString += $"{rank}. {entry.username}: {entry.highScore} **\n";
            }
            else
            {
                leaderboardString += $"{rank}. {entry.username}: {entry.highScore}\n";
            }
            rank++;
        }
        return leaderboardString;
    }

    public (int currentRank, int scoreToNextRank) GetUserLeaderboardInfo()
    {
        if (UserManager.Instance == null || leaderboardData == null || leaderboardData.entries == null)
        {
            return (-1, -1); // Indicate error or no data
        }

        string currentUser = UserManager.Instance.GetUsername();
        int curentScore = ScoreManager.Instance.GetScore();
        int userRank = -1;
        int scoreToNextRank = -1;

        // Sort the leaderboard entries by score in descending order
        List<LeaderboardEntry> sortedEntries = leaderboardData.entries.OrderByDescending(entry => entry.highScore).ToList();

        for (int i = 0; i < sortedEntries.Count; i++)
        {
            if (sortedEntries[i].username == currentUser)
            {
                userRank = i + 1;
                if (i > 0) // If not in the first place
                {
                    scoreToNextRank = sortedEntries[i - 1].highScore - curentScore + 100;
                }
                else
                {
                    scoreToNextRank = 0; // Already in first place
                }
                break;
            }
        }

        return (userRank, scoreToNextRank);
    }

    private void UpdateLeaderboard()
    {
        if (UserManager.Instance == null)
        {
            Debug.LogError("UserManager singleton instance not found. Cannot update leaderboard cache.");
            return;
        }

        // Get all user data from UserManager
        List<UserData> allUsers = GetAllUsers();

        // Use a dictionary to store the highest score for each user
        Dictionary<string, int> highestScores = new Dictionary<string, int>();
        foreach (UserData user in allUsers)
        {
            if (!highestScores.ContainsKey(user.username) || user.highScore > highestScores[user.username])
            {
                highestScores[user.username] = user.highScore;
            }
        }

        // Convert the dictionary to a list of LeaderboardEntry and sort
        List<LeaderboardEntry> leaderboardEntries = highestScores.Select(kvp => new LeaderboardEntry { username = kvp.Key, highScore = kvp.Value })
            .OrderByDescending(entry => entry.highScore)
            .ToList();

        // Create new LeaderboardData
        leaderboardData = new LeaderboardData
        {
            entries = leaderboardEntries,
            lastUpdated = DateTime.Now
        };
        SaveLeaderboard();

        // Update the previous user rank after the leaderboard is updated
        if (ScoreManager.Instance != null)
        {
            (previousUserRank, _) = GetUserLeaderboardInfo();
        }
    }

    private List<UserData> GetAllUsers()
    {
        if (UserManager.Instance != null)
        {
            Dictionary<string, UserData> users = UserManager.Instance.GetUsers();
            return users.Values.ToList();
        }
        else
        {
            Debug.LogError("UserManager singleton instance not found. Cannot retrieve user data.");
            return new List<UserData>();
        }
    }

    private void SaveLeaderboard()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(leaderboardSavePath, FileMode.Create);
        formatter.Serialize(stream, leaderboardData);
        stream.Close();
    }

    private void LoadLeaderboard()
    {
        if (File.Exists(leaderboardSavePath))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(leaderboardSavePath, FileMode.Open);
                leaderboardData = formatter.Deserialize(stream) as LeaderboardData;
                stream.Close();
            }
            catch (Exception e)
            {
                Debug.LogError("Error loading leaderboard: " + e.Message);
                leaderboardData = null; // Ensure leaderboardData is null on failure
            }
        }
        else
        {
            Debug.Log("No leaderboard data file found. Creating a new one.");
            leaderboardData = null; // Initialize to null
        }

        // Initialize previous user rank on load
        if (ScoreManager.Instance != null)
        {
            (previousUserRank, _) = GetUserLeaderboardInfo();
        }
    }
}


