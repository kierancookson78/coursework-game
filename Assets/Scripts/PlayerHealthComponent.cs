using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private TextMeshProUGUI currentUserText;
    [SerializeField] private TextMeshProUGUI currentUserHighScoreText;
    private int currentHealth;
    private bool isPlayerDead = false;

    void Start()
    {
        currentUserHighScoreText.text = "High Score: " + UserManager.Instance.GetHighScore().ToString();
        currentUserText.text = UserManager.Instance.GetUsername();
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Player jet took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
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
                UserManager.Instance.UpdateHighScore(newScore);
            }
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
}
