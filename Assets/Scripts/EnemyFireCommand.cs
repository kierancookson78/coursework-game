using UnityEngine;

// The enemy fire command fires their bullets from the nose of the jet.
public class EnemyFireCommand : ICommand
{
    private GameObject bulletPrefab;
    private Transform firePoint;
    private AudioClip bulletAudio;
    private float bulletSpeed;

    public EnemyFireCommand(GameObject bulletPrefab, Transform firePoint, AudioClip bulletAudio, float bulletSpeed)
    {
        this.bulletPrefab = bulletPrefab;
        this.firePoint = firePoint;
        this.bulletAudio = bulletAudio;
        this.bulletSpeed = bulletSpeed;
    }

    public void Execute()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            // Spawn the bullet at the nose.
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                // fire the bullet
                rb.linearVelocity = firePoint.up * bulletSpeed;
                PlayBulletShot();
            }
            else
            {
                Debug.LogError("Bullet prefab does not have a Rigidbody2D!");
            }
        }
        else
        {
            Debug.LogError("Bullet Prefab or Fire Point not assigned!");
        }
    }

    // Plays the bullet audio.
    private void PlayBulletShot()
    {
        if (bulletAudio != null)
        {
            AudioSource.PlayClipAtPoint(bulletAudio, firePoint.position);
        }
    }
}
