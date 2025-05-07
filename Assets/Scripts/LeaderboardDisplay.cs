using TMPro;
using UnityEngine;

public class LeaderboardDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leaderboardText;
    void Start()
    {
        LeaderboardService leaderboardService = FindFirstObjectByType<LeaderboardService>();
        leaderboardText.text = leaderboardService.GetLeaderboard();
    }
}
