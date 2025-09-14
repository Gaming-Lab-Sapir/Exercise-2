using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private GameObject bulletPrefab;

    private Animator animator;
    private SpriteRenderer sprite;
    private InputSystem inputActions;

    const float flipRotationX = 0f;
    const float flipRotationY = 180f; 
    const float flipRotationZ = 0f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        inputActions = new();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Shoot.performed += OnShootPerformed;
    }

    private void OnDisable()
    {
        inputActions.Player.Shoot.performed -= OnShootPerformed;
        inputActions.Player.Disable();
    }

    private void OnShootPerformed(InputAction.CallbackContext ctx)
    {
        animator?.SetTrigger("Shoot");

        Quaternion bulletRotation = shootingPoint.rotation;

        if (sprite != null && sprite.flipX)  
        {
            Quaternion flipRotation = Quaternion.Euler(flipRotationX, flipRotationY, flipRotationZ);
            bulletRotation = flipRotation * bulletRotation;
        }

        Instantiate(bulletPrefab, shootingPoint.position, bulletRotation);
    }
}
