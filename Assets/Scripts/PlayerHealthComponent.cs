using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private TextMeshProUGUI currentUserText;
    [SerializeField] private TextMeshProUGUI currentUserHighScoreText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;
    private int currentHealth;
    private LeaderboardService leaderboardService;
    private bool isPlayerDead = false;
    private Color lowHealthColor = Color.red;
    private Color highHealthColor = Color.green;

    void Start()
    {
        leaderboardService = FindFirstObjectByType<LeaderboardService>();
        currentUserHighScoreText.text = "High Score: " + UserManager.Instance.GetHighScore().ToString();
        currentUserText.text = UserManager.Instance.GetUsername();
        currentHealth = maxHealth;
        UpdateHealthBarColor();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player jet took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
        UpdateHealthBarColor();
        UpdateHealthBar();
    }

    public void Die()
    {
        int newScore = ScoreManager.Instance.GetScore();
        Debug.Log("Player jet destroyed!");
        if (!isPlayerDead)
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                PlayExplosion();
            }
            leaderboardService.AddScore(newScore);
            Invoke("GameOver", 0.767f);
        }
        isPlayerDead = true;
    }

    public void PlayExplosion()
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadSceneAsync(3);
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    void UpdateHealthBarColor()
    {
        if (fillImage != null)
        {
            // Calculate the health percentage (0 to 1)
            float healthPercentage = currentHealth / maxHealth;

            // Use Color.Lerp to smoothly interpolate between the two colors
            fillImage.color = Color.Lerp(lowHealthColor, highHealthColor, healthPercentage);
        }
    }
}
