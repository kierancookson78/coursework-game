using UnityEngine;

public class GroundColliderComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with an enemy jet
        PlayerHealthComponent playerJet = collision.GetComponent<PlayerHealthComponent>();

        if (playerJet != null)
        {
            // Damage the enemy jet
            playerJet.TakeDamage(100);
        }
    }
}
