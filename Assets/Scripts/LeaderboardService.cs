using UnityEngine;

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
