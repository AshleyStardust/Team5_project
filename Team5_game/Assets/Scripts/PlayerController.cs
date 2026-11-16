using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    public Animator JumpPadAnim;

    private float direction = 7f;

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float jumpSpeed = 6f;

    public bool isGrounded;
    public bool hasKey = false;

    /*gets the build index of the current scene in the build profile*/
    public int buildIndex { get; }

    public GameObject hasKeyImage;
    public GameObject LoseText;
    public GameObject JumpPad;
    public GameObject WinText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        LoseText.SetActive(false);
        hasKeyImage.SetActive(false);
        WinText.SetActive(false);
        JumpPadAnim = JumpPad.GetComponent<Animator>();
  
    }

    void Update()
    {
        direction = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
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

    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.name == "Ground")
        {
            isGrounded = true;
        }

        if (other.gameObject.name == "JumpPad")
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, (jumpSpeed + 5));
            JumpPadAnim.enabled = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Ground")
        {
            isGrounded = false;
        }

        if (other.gameObject == JumpPad)
        {
            JumpPadAnim.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp_Key"))
        {
            other.gameObject.SetActive(false);
            hasKey = true;
            hasKeyImage.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D other) { 
        
        if (other.gameObject.CompareTag("Ladder") && Input.GetKey(KeyCode.W))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, (jumpSpeed - 10));
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (isGrounded == false)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
                Destroy(other.gameObject);
            }

            if (isGrounded == true)
            {
                Destroy(this);
                spriteRenderer.flipY = true;
                anim.enabled = false;
                LoseText.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); /*line of code to reload current scene on player death after 3 secs*/
            }
            if (other.gameObject.name == "SpikyEnemy")
            {
                Destroy(this);
                spriteRenderer.flipY = true;
                anim.enabled = false;
                LoseText.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); /*line of code to reload current scene on player death after 3 secs*/
            }
        }

        if (other.gameObject.CompareTag("Spikes"))
        {
            Destroy(this);
            spriteRenderer.flipY = true;
            anim.enabled = false;
            LoseText.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); /*line of code to reload current scene on player death after 3 secs*/
        }

        /*loads next level if player collides with Door while having the key*/
        if (other.gameObject.name == "Door")
        {
            if (hasKey == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        if (other.gameObject.name == "FinalDoor")
        {
            if (hasKey == true)
            {
                Destroy(this);
                WinText.SetActive(true);
                anim.enabled = false;
                hasKeyImage.SetActive(false);
            }
        }
    }



}