using UnityEngine;

public class PlayerBulletColliderComponent : MonoBehaviour
{
    [SerializeField] private int bulletDamage;
    private PowerUpHotbar powerUpHotbar;

    void Start()
    {
        powerUpHotbar = FindAnyObjectByType<PowerUpHotbar>();
        if (powerUpHotbar.GetCannonStatus())
        {
            bulletDamage = bulletDamage * 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with an enemy jet
        EnemyHealthComponent enemyJet = collision.GetComponent<EnemyHealthComponent>(); 

        if (enemyJet != null)
        {
            enemyJet.TakeDamage(bulletDamage);

            Destroy(gameObject);
        }
    }
}
