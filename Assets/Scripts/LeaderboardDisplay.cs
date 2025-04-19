using TMPro;
using UnityEngine;

public class LeaderboardDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leaderboardText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LeaderboardService leaderboardService = FindFirstObjectByType<LeaderboardService>();
        leaderboardText.text = leaderboardService.GetLeaderboard();
    }
}
