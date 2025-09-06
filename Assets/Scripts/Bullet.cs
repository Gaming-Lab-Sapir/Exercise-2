using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float destroyDelay = 10f;
    [SerializeField] float speed = 12f;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, destroyDelay); 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>()?.HitByBullet();
            Destroy(gameObject); 
        }
    }
}
