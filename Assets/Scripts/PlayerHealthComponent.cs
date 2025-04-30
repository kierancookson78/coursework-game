using System.Collections;
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
    [SerializeField] private Image healthfillImage;
    [SerializeField] private Slider shieldSlider;
    private int currentHealth;
    private int shield = 50;
    private bool isShieldActive = false;
    private LeaderboardService leaderboardService;
    private bool isPlayerDead = false;
    private Color lowHealthColor = Color.red;
    private Color medHealthColor = Color.yellow;
    private Color highHealthColor = Color.green;
    private float cooldownTimer = 0;
    private float regenDelay = 1;

    void Start()
    {
        leaderboardService = FindFirstObjectByType<LeaderboardService>();
        currentUserHighScoreText.text = "High Score: " + UserManager.Instance.GetHighScore().ToString();
        currentUserText.text = UserManager.Instance.GetUsername();
        currentHealth = maxHealth;
        shieldSlider.gameObject.SetActive(false);
        UpdateHealthBarColor();
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (!Input.GetMouseButton(0) && cooldownTimer <= 0)
        {
            cooldownTimer = regenDelay;
            currentHealth += 2;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            UpdateHealthBarColor();
            UpdateHealthBar();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isShieldActive)
        {
            currentHealth -= damage;
            Debug.Log("Player jet took " + damage + " damage. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
            UpdateHealthBarColor();
            UpdateHealthBar();
        } else
        {
            shield -= damage;
            if (shield <= 0) 
            { 
                shieldSlider.gameObject.SetActive(false);
                isShieldActive = false;
            }
            shieldSlider.value = shield;
        }
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
        if (healthfillImage != null)
        {
            if (currentHealth > 50)
            {
                healthfillImage.color = highHealthColor;
            } else if (currentHealth <= 50 && currentHealth > 25)
            {
                healthfillImage.color = medHealthColor;
            }
            else if (currentHealth <= 25)
            {
                healthfillImage.color = lowHealthColor;
            }
        }
    }

    public void ActivateShield()
    {
        isShieldActive = true;
    }
}
