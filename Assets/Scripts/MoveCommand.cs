using UnityEngine;

public class MoveCommand : ICommand
{
    private Transform planeTransform;
    private Rigidbody2D rb;
    private Vector3 targetPosition;
    private float speed;
    private float rotationSpeed;

    public MoveCommand(Transform planeTransform ,Rigidbody2D rb, Vector3 targetPosition, float speed, float rotationSpeed)
    {
        this.planeTransform = planeTransform;
        this.rb = rb;
        this.targetPosition = targetPosition;
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
    }

    public void Execute()
    {
        Vector2 direction = targetPosition - planeTransform.position;
        float distanceToMouse = direction.magnitude;

        if (distanceToMouse > 0.1f)
        {
            direction.Normalize();
            rb.linearVelocity = direction * speed;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
            planeTransform.rotation = Quaternion.Slerp(planeTransform.rotation, Quaternion.Euler(0f, 0f, targetAngle), Time.fixedDeltaTime * rotationSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
