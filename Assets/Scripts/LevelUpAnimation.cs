using UnityEngine;
using System.Collections;
using TMPro;

public class LevelUpAnimation : MonoBehaviour
{
    [SerializeField] private GameObject upArrow;
    [SerializeField] private float animationDuration = 1.0f;
    [SerializeField] private float bounceHeight = 0.5f;
    [SerializeField] private int numberOfBounces = 3;
    [SerializeField] private AudioClip levelUpSound;
    [SerializeField] private TextMeshProUGUI rankUpText;
    private Vector3 initialPosition;
    private Vector3 initialPositionText;

    private RectTransform arrowRectTransform;
    private RectTransform rankUpTextTransform;

    void Start()
    {
        // Get the RectTransform in Awake for efficiency
        if (upArrow != null)
        {
            arrowRectTransform = upArrow.GetComponent<RectTransform>();
            rankUpTextTransform = rankUpText.GetComponent<RectTransform>();
            if (arrowRectTransform == null)
            {
                Debug.LogError("upArrow GameObject must have a RectTransform component!");
                upArrow = null; // Disable the script if no RectTransform
            }
        }
        else
        {
            Debug.LogError("upArrow GameObject is not assigned!  Please assign the arrow in the inspector.");
        }
        initialPosition = arrowRectTransform.anchoredPosition;
        initialPositionText = rankUpTextTransform.anchoredPosition;
        upArrow.SetActive(false);
    }

    // Call this function to start the level up animation
    public void PlayLevelUpAnimation()
    {
        if (upArrow != null)
        {
            // Start the coroutine, which handles the animation.
            StartCoroutine(AnimateArrow());
        }
    }

    // Coroutine for animating the arrow
    private IEnumerator AnimateArrow()
    {
        // Make sure the arrow is visible
        upArrow.SetActive(true);
        rankUpText.text = "You ranked up!";

        float timeElapsed = 0f;

        // Play sound effect if assigned
        if (levelUpSound != null)
        {
            AudioSource.PlayClipAtPoint(levelUpSound, Camera.main.transform.localPosition, 4f);
        }

        // Perform the bouncing animation
        while (timeElapsed < animationDuration)
        {
            timeElapsed += Time.deltaTime;
            // Calculate the vertical position using a sine wave for a bounce effect.
            float verticalOffset = Mathf.Abs(Mathf.Sin(timeElapsed * Mathf.PI * numberOfBounces / animationDuration)) * bounceHeight;
            arrowRectTransform.anchoredPosition = initialPosition + new Vector3(0, verticalOffset, 0);
            rankUpTextTransform.anchoredPosition = initialPositionText + new Vector3(0, verticalOffset, 0);
            yield return null; // Wait for the next frame.
        }

        // Make the arrow invisible after the animation.
        rankUpText.text = "";
        arrowRectTransform.anchoredPosition = initialPosition;
        rankUpTextTransform.anchoredPosition = initialPositionText;
        upArrow.SetActive(false);
    }
}

