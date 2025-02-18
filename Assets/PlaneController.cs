using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float thrustForce = 100f;
    public float rotationSpeed = 180f; // Degrees per second
    public float maxSpeed = 20f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Thrust
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * thrustForce);

            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }

        // Rotation (Improved)
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        direction.Normalize();

        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Apply Rotation *ONLY* if the angle difference is significant
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.z, targetAngle));  // Use DeltaAngle!

        if (angleDifference > 1f) // Adjust 1f for sensitivity. Smaller = more sensitive
        {
            float newAngle = Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0f, 0f, newAngle);
        }

    }
}
