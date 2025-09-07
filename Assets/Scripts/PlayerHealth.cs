using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    [SerializeField] float iFrames = 0.8f;
    [SerializeField] Image[] hpIcons;

    [Header("Game Over UI")]
    [SerializeField] GameObject gameOverText;

    [Header("Restart")]
    [SerializeField] float restartDelaySeconds = 2f; 

    int current;
    float lastHit = -999f;
    SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable() => GameEvents.PlayerDamaged += TakeDamage;
    void OnDisable() => GameEvents.PlayerDamaged -= TakeDamage;

    void Start()
    {
        Time.timeScale = 1f;

        current = maxHealth;
        RefreshUI();

        if (gameOverText != null)
            gameOverText.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        if (Time.time - lastHit < iFrames) return;
        lastHit = Time.time;

        current = Mathf.Max(0, current - amount);
        RefreshUI();
        StartCoroutine(HitFlash());

        if (current <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameOverText != null)
            gameOverText.SetActive(true);

        Time.timeScale = 0f;

        GameEvents.RaiseGameLost();

        var col = GetComponent<Collider2D>(); if (col) col.enabled = false;
        var move = GetComponent<PlayerMovement>(); if (move) move.enabled = false;
        var shoot = GetComponent<PlayerShoot>(); if (shoot) shoot.enabled = false;
        if (sr) sr.enabled = false;
    }

    void RefreshUI()
    {
        if (hpIcons == null) return;

        for (int i = 0; i < hpIcons.Length; i++)
        {
            if (hpIcons[i] != null)
                hpIcons[i].enabled = i < current;
        }
    }

    System.Collections.IEnumerator HitFlash()
    {
        var orig = sr.color;
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = orig;
    }
}
