using UnityEngine;

public class PlayerBulletColliderComponent : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 50; // Amount of damage the bullet deals

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with an enemy jet
        EnemyHealthComponent enemyJet = collision.GetComponent<EnemyHealthComponent>();

        if (enemyJet != null)
        {
            // Damage the enemy jet
            enemyJet.TakeDamage(bulletDamage);

            // Destroy the bullet (or deactivate it)
            Destroy(gameObject); // Or gameObject.SetActive(false); for object pooling
        }
    }
}
