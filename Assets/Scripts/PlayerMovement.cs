using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    bool isGrounded = true;
    float directionX;
    Animator animator;
    SpriteRenderer sprite;
    InputSystem inputActions;

    private void Awake()
    {
        inputActions = new();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ValidateComponent(rb, nameof(rb));
        animator = GetComponent<Animator>();
        ValidateComponent(animator, nameof(animator));
        sprite = GetComponent<SpriteRenderer>();
        ValidateComponent(sprite, nameof(sprite));
    }

    private void ValidateComponent<T>(T component, string name)
    {
        if (component is null)
        {
            Debug.LogError($"{name} is null!");
        }
    }

    void Update()
    {
        HandleMovementAnimations();
    }
    private void HandleMovementAnimations()
    {
        animator.SetFloat("Speed", rb.linearVelocity.x);
        if (directionX != 0)
            sprite.flipX = directionX < 0;
        
    }

    private void HandleJump()
    {
        isGrounded = false;
        animator.SetTrigger("IsJumping");
        rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new(directionX * speed, rb.linearVelocity.y);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    private void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Move.performed += OnMovePerformed;
        inputActions.Player.Move.canceled += OnMoveCanceled;
        inputActions.Player.Jump.performed += OnJumpPerformed;
    }
    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (!isGrounded) return;

        HandleJump();
    }

    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx)=> directionX = 0;

    private void OnMovePerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        directionX = ctx.ReadValue<Vector2>().x;
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
        inputActions.Player.Move.performed -= OnMovePerformed;
        inputActions.Player.Move.canceled -= OnMoveCanceled;
        inputActions.Player.Jump.performed -= OnJumpPerformed;
    }
}


