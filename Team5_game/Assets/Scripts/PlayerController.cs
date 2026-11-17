using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Variables for all components needed for the rest of the code
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;
    public Animator JumpPadAnim;

    //Variables for player speed, serialized so that they can be changed in the Unity editor if needed
    private float direction = 7f;
    [SerializeField]
    private float moveSpeed = 7f;
    [SerializeField]
    private float jumpSpeed = 6f;

    //Booleans needed for certain actions to occur
    public bool isGrounded;
    public bool hasKey = false;

    //Gets the build index of the current scene in the build profile
    public int buildIndex { get; }

    //Variables for all game objects needed for the rest of the code
    public GameObject hasKeyImage;
    public GameObject LoseText;
    public GameObject JumpPad;
    public GameObject WinText;

    void Start()
    {
        //Getting all components needed and assigning them to the variables
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        JumpPadAnim = JumpPad.GetComponent<Animator>();
        
        //Making sure certain game objects are inactive at the start of the game until requirements are met
        LoseText.SetActive(false);
        hasKeyImage.SetActive(false);
        WinText.SetActive(false);
    }

    void Update()
    {
        //Movement code, using horizontal axis to allow left and right movement with A and D keys
        direction = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(direction * moveSpeed, rb.linearVelocity.y);

        //Jump code, using isGrounded boolean so that the player can't keep jumping in the air
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
        }

        //Player sprite faces the right direction when moving
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
        //Making isGrounded true while player is on the ground
        if (other.gameObject.name == "Ground")
        {
            isGrounded = true;
        }

        //Player is launched upwards when colliding with jump pad in level 3, also enables jump pad animation
        if (other.gameObject == JumpPad)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, (jumpSpeed + 5));
            JumpPadAnim.enabled = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        //Making isGrounded false when player is not on the ground
        if (other.gameObject.name == "Ground")
        {
            isGrounded = false;
        }

        //Disabling jump pad animation once player has jumped on it
        if (other.gameObject == JumpPad)
        {
            JumpPadAnim.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Adds key to UI inventory in top left and removes key pickup from level once player collides with it
        if (other.gameObject.CompareTag("PickUp_Key"))
        {
            other.gameObject.SetActive(false);
            hasKey = true;
            hasKeyImage.SetActive(true);
        }
    }

    void OnTriggerStay2D(Collider2D other) 
    { 
        //Movement code for climbing ladder in level 2 using jumpspeed variable for vertical speed while W is pressed
        if (other.gameObject.CompareTag("Ladder") && Input.GetKey(KeyCode.W))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, (jumpSpeed - 10));
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        //Destroys enemy if player jumps on their head and uses jump code again to make player bounce off enemy after destroying it
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (isGrounded == false)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed);
                Destroy(other.gameObject);
            }

            //Destroys player if colliding with enemy without jumping on them
            //Activates lose text and death animation by flipping sprite and disabling walking animation
            if (isGrounded == true)
            {
                Destroy(this);
                spriteRenderer.flipY = true;
                anim.enabled = false;
                LoseText.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reloads current scene on player death
            }

            //Code for enemy with spike on its shell in level 2 so that the player dies even if jumping on it
            //Activates lose text and death animation by flipping sprite and disabling walking animation
            if (other.gameObject.name == "SpikyEnemy")
            {
                Destroy(this);
                spriteRenderer.flipY = true;
                anim.enabled = false;
                LoseText.SetActive(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reloads current scene on player death                                                                              
            }
        }

        //Destroys player if colliding with spikes
        //Activates lose text and death animation by flipping sprite and disabling walking animation
        if (other.gameObject.CompareTag("Spikes"))
        {
            Destroy(this);
            spriteRenderer.flipY = true;
            anim.enabled = false;
            LoseText.SetActive(true);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Reloads current scene on player death
        }

        //Loads next level if player collides with Door while having the key
        if (other.gameObject.name == "Door")
        {
            if (hasKey == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }

        //Code for door in level 3 to stop game and display win text
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