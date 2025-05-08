using UnityEngine;

// Implementation of the service locator pattern for a leaderboard adapted from "7_Singleton_ServiceLocator" by Sean Walton
// at: https://canvas.swansea.ac.uk/courses/52796/files/7564778?module_item_id=3014035
public class LeaderboardService : MonoBehaviour
{
    private ILeaderboard abstractLeaderboard;
    void Awake()
    {
        abstractLeaderboard = GetComponent<ILeaderboard>();
    }

    public void AddScore(int score)
    {
        abstractLeaderboard.AddScore(score);
    }

    public string GetLeaderboard()
    {
        return abstractLeaderboard.GetLeaderboard();
    }

    public (int currentRank, int scoreToNextRank) GetUserLeaderboardInfo()
    {
        return abstractLeaderboard.GetUserLeaderboardInfo();
    }
}
