using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 700f;
    [SerializeField] private Transform feet;
    [SerializeField] private float baseGravityScale = 1f; // the default gravity scale
    [SerializeField] private float maxGravityScale = 3f; // the maximum gravity scale
    [SerializeField] private float gravityScaleIncrement = 0.2f; // the amount to increase gravity scale per FixedUpdate() while airborne

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float horizontalInput;
    private bool facingRight = true;
    private Animator animator;
    private float currentGravityScale;
    private float timeInAir = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        currentGravityScale = baseGravityScale;
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        Move();
        CheckGround();

        // If the character is not grounded, increase gravity scale
        if (!isGrounded)
        {
            timeInAir += Time.deltaTime;
            animator.SetBool("isJumping", true);

            if (timeInAir > .35f) {
                currentGravityScale += gravityScaleIncrement * Time.fixedDeltaTime;
                currentGravityScale = Mathf.Min(currentGravityScale, maxGravityScale);
                rb.gravityScale = baseGravityScale * currentGravityScale;
            }
        }
        else
        {
            timeInAir = 0f;
            currentGravityScale = baseGravityScale;
            rb.gravityScale = baseGravityScale;
            animator.SetBool("isJumping", false);
        }
    }

    void Move()
    {
        if (rb.velocity.x > .1f || rb.velocity.x < -.1f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime);
        animator.SetBool("isJumping", true);
        animator.SetBool("isRunning", false);
    }

    void CheckGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(feet.transform.position, Vector2.down, 1.1f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    void Flip()
    {
        // Toggle the facingRight boolean
        facingRight = !facingRight;

        // Flip the character's local scale on the X-axis
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
