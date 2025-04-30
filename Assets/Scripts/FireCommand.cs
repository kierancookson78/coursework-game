using UnityEngine;

public class FireCommand : ICommand
{
    private GameObject bulletPrefab;
    private Transform firePoint;
    private AudioClip bulletAudio;
    private float bulletSpeed;
    private PowerUpHotbar powerUpHotbar;

    public FireCommand(GameObject bulletPrefab, Transform firePoint, AudioClip bulletAudio, float bulletSpeed, PowerUpHotbar powerUpHotbar)
    {
        this.bulletPrefab = bulletPrefab;
        this.firePoint = firePoint;
        this.bulletAudio = bulletAudio;
        this.bulletSpeed = bulletSpeed;
        this.powerUpHotbar = powerUpHotbar;
    }

    public void Execute()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            bool isCannonActive = powerUpHotbar.GetCannonStatus();
            GameObject bullet = GameObject.Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            if (isCannonActive)
            {
                bullet.transform.localScale = new (10f, 10f, 10f);
            }

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
