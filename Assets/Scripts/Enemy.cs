using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float destroyDelay = 0.5f;
    [SerializeField] float bulletHitDelay = 0.05f;
    private bool isActive = true;
    Rigidbody2D rb;
    Animator animator;

    public event Action OnEnemyDestroyed;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (isActive)
        {
            rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y); 
            if (animator != null) animator.SetBool("IsWalking", true);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            if (animator != null) animator.SetBool("IsWalking", false);
        }
    }

    public void HitByBullet()
    {
        if (!isActive) return;
        isActive = false;

        GameEvents.RaiseEnemyKilledByBullet();

        StartCoroutine(WaitToDestroy(bulletHitDelay));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameEvents.RaisePlayerDamaged(1);

            GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(WaitToDestroy(destroyDelay));
        }
    }

    IEnumerator WaitToDestroy(float t)
    {
        yield return new WaitForSeconds(t);
        OnEnemyDestroyed?.Invoke();
        Destroy(gameObject);
    }
}
