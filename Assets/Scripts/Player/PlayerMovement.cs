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

    public bool canMove = true;

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

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
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

    private void FixedUpdate()
    {
        if(canMove)
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
        if(canMove)
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
        if(canMove)
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
}
