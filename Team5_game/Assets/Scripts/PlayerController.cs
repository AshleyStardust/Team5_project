using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AdaptivePerformance.Provider;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    private float direction = 7f;
    //public float elapsed;

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private float jumpSpeed = 6f;

    public bool isGrounded;
    public bool hasKey = false;

    /*gets the build index of the current scene in the build profile*/
    public int buildIndex { get; }
   

    public GameObject hasKeyImage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        hasKeyImage.SetActive(false);
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

    void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); /*line of code to reload current scene on player death after 3 secs*/
            }
        }

        if (other.gameObject.CompareTag("Spikes"))
        {
            Destroy(this);
            spriteRenderer.flipY = true;
            anim.enabled = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); /*line of code to reload current scene on player death after 3 secs*/

        }

        /*loads next level if player collides with Door while having the key*/
        if (other.gameObject.name=="Door")
        {
            if (hasKey == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }



}