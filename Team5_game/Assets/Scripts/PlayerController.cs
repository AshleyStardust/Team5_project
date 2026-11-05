using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private float direction = 7f;

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float jumpSpeed = 6f;

    bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
            isGrounded = false;
        }

        if (direction > 0)
        {
            spriteRenderer.flipX = true;
        }

        if (direction < 0)
        {
            spriteRenderer.flipX = false;
        }
    }
<<<<<<< HEAD

    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
=======
>>>>>>> death-by-spikes-code
}