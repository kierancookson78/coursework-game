using UnityEngine;

public class InfiniteGroundSimple : MonoBehaviour
{
    private Transform cameraTransform;
    private float initialYPosition;
    private SpriteRenderer groundRenderer;

    void Start()
    {
        cameraTransform = Camera.main.transform;
        initialYPosition = transform.position.y;
        groundRenderer = GetComponent<SpriteRenderer>();

        if (groundRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the ground object!");
            return;
        }

        ScaleGroundToCameraWidth();
    }

    void Update()
    {
        transform.position = new Vector3(cameraTransform.position.x, initialYPosition, transform.position.z);
    }

    void ScaleGroundToCameraWidth()
    {
        if (Camera.main != null && groundRenderer != null)
        {
            float cameraWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
            float groundWidth = groundRenderer.bounds.size.x; // Get the ground's current width

            if (groundWidth > 0)
            {
                float scaleFactor = (cameraWidth / groundWidth) * 2.5f;
                transform.localScale = new Vector3(scaleFactor, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                Debug.LogError("Ground width is zero. Check the SpriteRenderer's bounds.");
            }
        }
    }
}
