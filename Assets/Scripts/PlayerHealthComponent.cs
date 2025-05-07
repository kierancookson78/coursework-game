using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private AudioClip shieldSound;

    [Header("HUD Elements")]
    [SerializeField] private TextMeshProUGUI currentUserText;
    [SerializeField] private TextMeshProUGUI currentUserHighScoreText;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image healthfillImage;
    [SerializeField] private Slider shieldSlider;

    private AudioSource shieldSource;
    private int currentHealth;
    private int shield = 50;
    private bool isShieldActive = false;
    private bool isPlayerDead = false;
    private Color lowHealthColor = Color.red;
    private Color medHealthColor = Color.yellow;
    private Color highHealthColor = Color.green;
    private float cooldownTimer = 0;
    private float regenDelay = 1;
    private float duration = 2.5f;
    private float _elapsedTime;
    private float _startTime;
    private float _startValue;
    private float _targetValue;

    void Start()
    {
        currentUserHighScoreText.text = "High Score: " + UserManager.Instance.GetHighScore().ToString();
        currentUserText.text = UserManager.Instance.GetUsername();
        shieldSource = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        shieldSlider.gameObject.SetActive(false);
        Cursor.visible = false;
        UpdateHealthBarColor();

        shieldSlider.value = 0f;
        _startValue = 0f;
        _elapsedTime = 0f;
        _targetValue = shieldSlider.maxValue;
    }

    void Update()
    {
        cooldownTimer -= Time.deltaTime;
        if (!Input.GetMouseButton(0) && cooldownTimer <= 0)
        {
            cooldownTimer = regenDelay;
            currentHealth += 4;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            UpdateHealthBarColor();
            UpdateHealthBar();
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isShieldActive)
        {
            currentHealth -= damage;
            Debug.Log("Player jet took " + damage + " damage. Current health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
            UpdateHealthBarColor();
            UpdateHealthBar();
        } else
        {
            shield -= damage;
            shieldSlider.value = shield;
            if (shield <= 0) 
            {
                shieldSlider.value = 0;
                shieldSlider.gameObject.SetActive(false);
                isShieldActive = false;
                shield = 50;
            }
        }
    }

    public void Die()
    {
        Debug.Log("Player jet destroyed!");
        if (!isPlayerDead)
        {
            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                PlayExplosion();
            }
            Invoke("GameOver", 0.767f);
        }
        isPlayerDead = true;
    }

    public void PlayExplosion()
    {
        if (audioClip != null)
        {
            AudioSource.PlayClipAtPoint(audioClip, transform.position);
        }
    }

    public void GameOver()
    {
        SceneManager.LoadSceneAsync(3, LoadSceneMode.Single);
        Cursor.visible = true;
    }

    void UpdateHealthBar()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHealth;
        }
    }

    void UpdateHealthBarColor()
    {
        if (healthfillImage != null)
        {
            if (currentHealth > 50)
            {
                healthfillImage.color = highHealthColor;
            } else if (currentHealth <= 50 && currentHealth > 25)
            {
                healthfillImage.color = medHealthColor;
            }
            else if (currentHealth <= 25)
            {
                healthfillImage.color = lowHealthColor;
            }
        }
    }

    public void ActivateShield()
    {
        isShieldActive = true;
    }

    public void StartFillingToValue(float targetValue)
    {
        _startTime = Time.time;
        _startValue = shieldSlider.value;
        _targetValue = targetValue;
        _elapsedTime = 0f;
        shieldSource.PlayOneShot(shieldSound);
        StartCoroutine(UpdateSlider());
    }

    private IEnumerator UpdateSlider()
    {
        if (shieldSlider == null) yield break;

        isShieldActive = false;

        while (_elapsedTime < duration)
        {
            _elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(_elapsedTime / duration);
            float newValue = Mathf.Lerp(_startValue, _targetValue, t);
            shieldSlider.value = newValue;
            yield return null;
        }
        shieldSlider.value = _targetValue;
        isShieldActive = true;
    }
}
