using UnityEngine;

public class AdaptOrthographicSize : MonoBehaviour
{
    public float targetAspectRatio = 16f / 9f; // Your desired "base" aspect ratio

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        AdjustOrthographicCamera();
    }

    void Update()
    {
        // You might want to call this in Update if the screen can be resized
        if ((float)Screen.width / Screen.height != cam.aspect)
        {
            AdjustOrthographicCamera();
        }
    }

    void AdjustOrthographicCamera()
    {
        float currentAspectRatio = cam.aspect;

        if (currentAspectRatio > targetAspectRatio)
        {
            // Screen is wider than the target, adjust height (orthographicSize) to fit width
            cam.orthographicSize = cam.orthographicSize * targetAspectRatio / currentAspectRatio;
        }
        else
        {
            // Screen is taller than or equal to the target, adjust width to fit height
            // We need to increase the orthographicSize to see more vertical content
            // so the wider screen fits within the increased vertical view.
            cam.orthographicSize = cam.orthographicSize / (targetAspectRatio / currentAspectRatio);
            // Which simplifies to:
            // cam.orthographicSize = cam.orthographicSize * currentAspectRatio / targetAspectRatio;
        }
    }
}
