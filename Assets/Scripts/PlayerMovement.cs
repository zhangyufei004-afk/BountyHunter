using NUnit.Framework;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float horizontal;
    private bool isFacingRight = true;

    [Header("Changable Variables")]
    [SerializeField] private float speed = 8f;
    [SerializeField] private float JumpPower = 16f;

    [Header("Attachments")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    [Header("Input Actions References")]
    [SerializeField] private InputActionReference jumpReference;

    void OnEnable()
    {
        jumpReference.action.performed += Jump;
        jumpReference.action.canceled += JumpCancelled;
    }
    
    void OnDisable()
    {
        jumpReference.action.performed -= Jump;
        jumpReference.action.canceled -= JumpCancelled;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpPower);
        }
    }

    private void JumpCancelled(InputAction.CallbackContext context)
    {
        if(rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<float>();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.transform.position, 0.2f);
    }
}
