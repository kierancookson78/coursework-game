using UnityEngine;

// Used to detect if enemy has hit the player with their bullet.
public class EnemyBulletColliderComponent : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 50;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player jet.
        PlayerHealthComponent playerJet = collision.GetComponent<PlayerHealthComponent>();

        if (playerJet != null)
        {
            // Damage the player jet
            playerJet.TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
    }
}
