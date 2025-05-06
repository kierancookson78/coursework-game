using UnityEngine;

public interface ILeaderboard
{
    void AddScore(int score);
    string GetLeaderboard();
    (int currentRank, int scoreToNextRank) GetUserLeaderboardInfo();
}
