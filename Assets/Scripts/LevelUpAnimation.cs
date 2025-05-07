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
    private AudioSource levelUpAudioSource;

    private RectTransform arrowRectTransform;
    private RectTransform rankUpTextTransform;

    void Start()
    {
        if (upArrow != null)
        {
            arrowRectTransform = upArrow.GetComponent<RectTransform>();
            rankUpTextTransform = rankUpText.GetComponent<RectTransform>();
            levelUpAudioSource = GetComponent<AudioSource>();
            if (arrowRectTransform == null)
            {
                Debug.LogError("upArrow GameObject must have a RectTransform component!");
                upArrow = null;
            }
        }
        else
        {
            Debug.LogError("upArrow GameObject is not assigned!  Please assign the arrow in the inspector.");
        }
        initialPosition = arrowRectTransform.anchoredPosition;
        initialPositionText = rankUpTextTransform.anchoredPosition;
        upArrow.SetActive(false);
        rankUpText.text = "";
    }

    public void PlayLevelUpAnimation()
    {
        if (upArrow != null)
        {
            StartCoroutine(AnimateArrow());
        }
    }

    private IEnumerator AnimateArrow()
    {
        upArrow.SetActive(true);
        rankUpText.text = "You ranked up!";

        float timeElapsed = 0f;

        if (levelUpSound != null)
        {
            levelUpAudioSource.PlayOneShot(levelUpSound);
        }

        // Perform the bouncing animation
        while (timeElapsed < animationDuration)
        {
            timeElapsed += Time.deltaTime;
            // Calculate the vertical position using a sine wave for a bounce effect.
            float verticalOffset = Mathf.Abs(Mathf.Sin(timeElapsed * Mathf.PI * numberOfBounces / animationDuration)) * bounceHeight;
            arrowRectTransform.anchoredPosition = initialPosition + new Vector3(0, verticalOffset, 0);
            rankUpTextTransform.anchoredPosition = initialPositionText + new Vector3(0, verticalOffset, 0);
            yield return null;
        }

        // Make the arrow invisible after the animation.
        rankUpText.text = "";
        arrowRectTransform.anchoredPosition = initialPosition;
        rankUpTextTransform.anchoredPosition = initialPositionText;
        upArrow.SetActive(false);
    }
}

