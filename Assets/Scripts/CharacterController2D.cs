using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 700f;
    [SerializeField] private Transform feet;
    [SerializeField] private GameObject shadow;

    private Rigidbody2D rb;
    private bool isGrounded = false;
    private float horizontalInput;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        MoveShadow();

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
    }

    void Move()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void MoveShadow() {
        RaycastHit2D hit = Physics2D.Raycast(feet.transform.position, Vector2.down, 7f, LayerMask.GetMask("Ground"));

        if (hit.collider != null)
        {
            shadow.SetActive(true);
            shadow.transform.position = hit.point;
        }
        else
        {
            shadow.SetActive(false);
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce));
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
