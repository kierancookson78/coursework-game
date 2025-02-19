using UnityEngine;

public class ShootBullets : MonoBehaviour
{
    public Vector3 bulletOffset = new(0, 0.5f, 0);
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float cooldownTimer = 0;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (Input.GetMouseButton(0) && cooldownTimer <= 0)
        {
            cooldownTimer = fireRate;
            Vector3 offset = transform.rotation * bulletOffset;
            Instantiate(bulletPrefab, transform.position +  offset, transform.rotation);
        }
    }
}