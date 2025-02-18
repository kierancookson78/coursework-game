using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float rotationSpeed = 5f; // For smooth rotation

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the plane!");
        }
    }

    void FixedUpdate()
    {
        // Mouse Direction
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float distanceToMouse = direction.magnitude; // Calculate distance

        // Set Velocity (Only if not close to the mouse)
        if (distanceToMouse > 0.1f) // Adjust 0.1f to your desired threshold
        {
            direction.Normalize();
            rb.linearVelocity = direction * moveSpeed;

            // Rotation (Preventing Flip, Smooth Rotation)
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, targetAngle), Time.fixedDeltaTime * rotationSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero; // Stop the plane when close
        }
    }
}
