using UnityEngine;

public class EnemyBulletColliderComponent : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 50; // Amount of damage the bullet deals

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with an enemy jet
        PlayerHealthComponent playerJet = collision.GetComponent<PlayerHealthComponent>();

        if (playerJet != null)
        {
            // Damage the enemy jet
            playerJet.TakeDamage(bulletDamage);

            // Destroy the bullet (or deactivate it)
            Destroy(gameObject); // Or gameObject.SetActive(false); for object pooling
        }
    }
}
