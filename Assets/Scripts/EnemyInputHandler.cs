using UnityEngine;

public class EnemyInputHandler : MonoBehaviour
{
    [SerializeField] private Rigidbody2D planeRigidbody;
    [SerializeField] private Transform planeTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip bulletAudio;
    [SerializeField] private Transform player;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float fireRate = 0.2f;
    [SerializeField] private float detectionDistance = 10f;
    [SerializeField] private float angleTolerance = 5f;
    private float cooldownTimer = 0;

    private ICommand moveCommand;
    private ICommand fireCommand;

    void Start()
    {
        if (planeRigidbody == null)
        {
            Debug.LogError("Plane transform not assigned");
        }
        if (firePoint == null)
        {
            Debug.LogError("Fire point not assigned");
        }
    }

    void FixedUpdate()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Mouse Movement
        Vector3 playerPosition = player.position;

        moveCommand = new EnemyMoveCommand(planeTransform, planeRigidbody, playerPosition, moveSpeed, rotationSpeed, detectionDistance); // Pass rotation speed
        moveCommand.Execute();

        // Left Mouse Button Click
        cooldownTimer -= Time.deltaTime;
        if (IsFacingPlayer() && cooldownTimer <= 0)
        {
            cooldownTimer = fireRate;
            fireCommand = new EnemyFireCommand(bulletPrefab, firePoint, bulletAudio, bulletSpeed);
            fireCommand.Execute();
        }
    }

    public bool IsFacingPlayer()
    {
        Vector2 directionToPlayer = player.position - transform.position;
        Vector2 planeForward = planeTransform.up;
        float angle = Vector2.Angle(planeForward, directionToPlayer);

        return angle <= angleTolerance;
    }
}
