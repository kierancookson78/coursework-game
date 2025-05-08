using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    private float accum = 0;
    private int frames = 0;
    private float fps;
    private float interval = 0.2f;
    private float timeElapsed;

    void Start()
    {
        if (fpsText == null)
        {
            Debug.LogError("FPSDisplay: fpsText not assigned.");
            enabled = false;
            return;
        }
        timeElapsed = 0;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        frames++;
        fps = accum / frames;
        if (timeElapsed >= interval)
        {
            fpsText.text = Mathf.Round(fps).ToString() + " FPS";
            timeElapsed = 0f;
        }

        accum = 0.0f;
        frames = 0;
    }
}

