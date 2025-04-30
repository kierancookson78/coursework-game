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
            Debug.LogError("UserManager instance not found. Cannot update high score.");
        }
    }

    public string GetLeaderboard()
    {
        string currentUser = UserManager.Instance.GetUsername();
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

    private void UpdateLeaderboard()
    {
        if (UserManager.Instance == null)
        {
            Debug.LogError("UserManager instance not found. Cannot update leaderboard cache.");
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
    }

    private List<UserData> GetAllUsers()
    {
        Dictionary<string, UserData> users = UserManager.Instance.GetUsers();
        return users.Values.ToList();
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
    }
}


