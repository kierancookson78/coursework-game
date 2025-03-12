using UnityEngine;

public class GroundColliderComponent : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerHealthComponent playerJet = collision.GetComponent<PlayerHealthComponent>();
        PlayerBulletColliderComponent playerBulletColliderComponent = collision.GetComponent<PlayerBulletColliderComponent>();
        if (playerBulletColliderComponent != null)
        {
            Destroy(playerBulletColliderComponent.gameObject);
        }

        if (playerJet != null)
        {
            playerJet.Die();
        }

        EnemyHealthComponent enemyJet = collision.GetComponent<EnemyHealthComponent>();
        EnemyBulletColliderComponent enemyBulletColliderComponent = collision.GetComponent<EnemyBulletColliderComponent>();
        if (enemyBulletColliderComponent != null)
        {
            Destroy(enemyBulletColliderComponent.gameObject);
        }

        if (enemyJet != null)
        {
            enemyJet.Die();
        }
    }
}
