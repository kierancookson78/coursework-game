using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target; // The target GameObject to follow
    [SerializeField] private Vector3 offset; // The offset from the target's position
    [SerializeField] private float smoothSpeed = 0.125f; // Smoothing factor for the camera movement
    [SerializeField] private bool lookAtTarget = true; // Whether the camera should look at the target

    private Vector3 desiredPosition;
    private Vector3 smoothedPosition;

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("CameraFollow: Target is not assigned!");
            return;
        }

        desiredPosition = target.position + offset;
        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}
