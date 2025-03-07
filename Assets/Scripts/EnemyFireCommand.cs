using UnityEngine;

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
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
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

    private void PlayBulletShot()
    {
        if (bulletAudio != null)
        {
            AudioSource.PlayClipAtPoint(bulletAudio, firePoint.position);
        }
    }
}
