using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float speed;
    private Rigidbody2D rb;
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity=transform.right*speed;
    }

    void Update()
    {
        
    }
}
