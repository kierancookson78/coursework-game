using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D planeRigidbody;
    [SerializeField] private Transform planeTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip bulletAudio;
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float fireRate = 0.2f;
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

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Mouse Movement
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        moveCommand = new MoveCommand(planeTransform ,planeRigidbody, mousePosition, moveSpeed, rotationSpeed);
        moveCommand.Execute();

        // Left Mouse Button Click
        cooldownTimer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && cooldownTimer <= 0)
        {
            cooldownTimer = fireRate;
            fireCommand = new FireCommand(bulletPrefab, firePoint, bulletAudio, bulletSpeed);
            fireCommand.Execute();
        }
    }
}
