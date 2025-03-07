using UnityEngine;

public class InfiniteBackground : MonoBehaviour
{
    [SerializeField] private Renderer backgroundRenderer;
    [SerializeField] private Transform planeTransform;
    [SerializeField] private float scrollSpeedMultiplier;

    private Vector2 startOffset;

    void Start()
    {
        if (backgroundRenderer == null)
        {
            backgroundRenderer = GetComponent<Renderer>();
            if (backgroundRenderer == null)
            {
                Debug.LogError("ScrollingBackground: No Renderer component found.");
                enabled = false;
                return;
            }
        }

        if (planeTransform == null)
        {
            Debug.LogError("ScrollingBackground: Plane Transform is not assigned.");
            enabled = false;
            return;
        }

        startOffset = backgroundRenderer.material.mainTextureOffset;
    }

    void Update()
    {
        // Calculate the scroll direction based on the plane's velocity
        Vector3 planeVelocity = planeTransform.GetComponent<Rigidbody2D>().linearVelocity; //Or Rigidbody for 3d

        // Calculate the scroll offset based on the plane's velocity and the multiplier
        Vector2 scrollOffset = new Vector2(planeVelocity.x * scrollSpeedMultiplier * Time.deltaTime, planeVelocity.y * scrollSpeedMultiplier * Time.deltaTime);

        // Apply the new offset to the material, adding to the current offset.
        backgroundRenderer.material.mainTextureOffset += scrollOffset;
    }

    public void SetScrollSpeedMultiplier(float newMultiplier)
    {
        scrollSpeedMultiplier = newMultiplier;
    }
}
