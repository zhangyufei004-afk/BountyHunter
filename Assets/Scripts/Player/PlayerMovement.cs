using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private bool isFacingRight = true;
    private float count = 0;

    [Header("Changable Variables")]
    public FloatReference speed;
    [SerializeField] private float JumpPower = 16f;
    public FloatReference maxJumps;

    [Header("Attachments")]
    public GameObject cameraAttachment;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private Animator animator;
    public AudioSource footsteps;
    public AudioSource jumpSound;

    // private bool canDash = true;
    // public bool isDashing;
    // public float dashPower = 24f;
    // public float dashTime = 0.2f;
    // public float dashCooldown = 1f;

    // public TrailRenderer tr;

    public bool canMove = true;

    [Header("Input Actions References")]
    [SerializeField] private InputActionReference jumpReference;
    // [SerializeField] private InputActionReference dashReference;

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

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameObject.GetComponent<PlayerHealth>().isDead)
        {
            Flip();

            if(isGrounded())
            {
                count = 0;
                animator.SetBool("isGrounded", true);
                animator.SetBool("isJumping", false);
            }
            else if(!isGrounded())
            {
                animator.SetBool("isGrounded", false);
                animator.SetBool("isJumping", true);
            }
        }
        // if(isDashing)
        // {
        //     return;
        // }


        // if(dashReference.action.triggered && canDash)
        // {
        //     StartCoroutine(Dash());
        // }
    }

    private void FixedUpdate()
    {
        if(canMove && !gameObject.GetComponent<PlayerHealth>().isDead)
        {
            rb.velocity = new Vector2(horizontal * speed.Value, rb.velocity.y);
        }
    }

    public bool isGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheck.localScale, 0, groundLayer);
    }

    private void Flip()
    {
        if(isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
            cameraAttachment.transform.Rotate(0f, 180f, 0f);
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if(canMove && !gameObject.GetComponent<PlayerHealth>().isDead)
        {
            if(isGrounded())
            {
                jumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
                count++;
            }
            else if(count < maxJumps.Value)
            {
                jumpSound.Play();
                rb.velocity = new Vector2(rb.velocity.x, JumpPower);
                count++;
            }
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
        if(canMove &&!gameObject.GetComponent<PlayerHealth>().isDead)
        {
            if(context.performed)
            {
                animator.SetFloat("isRunning", 1f);
                horizontal = context.ReadValue<float>();
            }
            else
            {
                animator.SetFloat("isRunning", 0f);
                horizontal = context.ReadValue<float>();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(groundCheck.position, groundCheck.localScale);
    }

    public void PlayFootsteps()
    {
        footsteps.Play();
    }

    // IEnumerator Dash()
    // {
    //     canDash = false;
    //     isDashing = true;
    //     float originalGravity = rb.gravityScale;
    //     rb.gravityScale = 0;
    //     // rb.velocity = new Vector2(transform.localScale.x * dashPower, 0f);
    //     rb.AddForce(transform.right * dashPower, ForceMode2D.Impulse);
    //     tr.emitting = true;
    //     yield return new WaitForSeconds(dashTime);
    //     tr.emitting = false;
    //     rb.gravityScale = originalGravity;
    //     isDashing = false;
    //     yield return new WaitForSeconds(dashCooldown);
    //     canDash = true;
    // }
}
