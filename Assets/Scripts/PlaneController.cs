using UnityEngine;

public class PlaneController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 5f;

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
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - transform.position;
        float distanceToMouse = direction.magnitude;

        if (distanceToMouse > 0.1f)
        {
            direction.Normalize();
            rb.linearVelocity = direction * moveSpeed;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, targetAngle), Time.fixedDeltaTime * rotationSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
