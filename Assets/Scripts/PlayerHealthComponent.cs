using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip audioClip;
    private int currentHealth;
    private bool isPlayerDead = false;

    void Start()
    {
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

    void Die()
    {
        Debug.Log("Player jet destroyed!");
        if (!isPlayerDead)
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                PlayExplosion();
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
