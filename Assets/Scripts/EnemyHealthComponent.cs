using TMPro;
using UnityEngine;

public class EnemyHealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip audioClip;
    private TextMeshProUGUI score;
    private int currentHealth;
    private static int playerScore = 0;

    void Start()
    {
        currentHealth = maxHealth;
        score = GetComponent<TextMeshProUGUI>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Enemy jet took " + damage + " damage. Current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy jet destroyed!");
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            PlayExplosion();
        }
        playerScore += 100;
        score.text = playerScore.ToString();
        Destroy(gameObject);
    }

    public void PlayExplosion()
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        }
    }
}
