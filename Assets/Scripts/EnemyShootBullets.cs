using UnityEngine;

public class EnemyShootBullets : MonoBehaviour
{
    [SerializeField] private Vector3 bulletOffset = new(0.5f, 1.542f, 0);
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Transform player;
    [SerializeField] private float angleTolerance;
    private float cooldownTimer = 0;

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (IsFacingPlayer() && cooldownTimer <= 0)
        {
            cooldownTimer = fireRate;
            Vector3 offset = transform.rotation * bulletOffset;
            Instantiate(bulletPrefab, transform.position + offset, transform.rotation);
            PlayBulletShot();
        }
    }

    public void PlayBulletShot()
    {
        if (audioClip != null)
        {
            if (audioSource != null)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
            else
            {
                AudioSource.PlayClipAtPoint(audioClip, transform.position);
            }
        }
        else
        {
            Debug.LogError("Audio Clip is not assigned!");
        }
    }

    public bool IsFacingPlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        Vector2 planeForward = transform.up;
        float angle = Vector2.Angle(planeForward, directionToPlayer);

        return angle <= angleTolerance;
    }
}
