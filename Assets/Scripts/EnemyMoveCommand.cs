using Unity.VisualScripting;
using UnityEngine;

public class EnemyMoveCommand : ICommand
{
    private Transform planeTransform;
    private Rigidbody2D rb;
    private Vector3 targetPosition;
    private float speed;
    private float rotationSpeed;
    private float detectionDistance;

    public EnemyMoveCommand(Transform planeTransform, Rigidbody2D rb, Vector3 targetPosition, float speed, float rotationSpeed, float detectionDistance)
    {
        this.planeTransform = planeTransform;
        this.rb = rb;
        this.targetPosition = targetPosition;
        this.speed = speed;
        this.rotationSpeed = rotationSpeed;
        this.detectionDistance = detectionDistance;
    }

    public void Execute()
    {
        Vector2 direction = targetPosition - planeTransform.position;
        float distanceToPlayer = direction.magnitude;

        if (distanceToPlayer < detectionDistance)
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
