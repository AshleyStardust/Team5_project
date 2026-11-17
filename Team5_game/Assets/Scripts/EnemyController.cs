using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyController : MonoBehaviour
{
    public float speed;

    //Variables for all components and game objects needed
    public GameObject pointA;
    public GameObject pointB;
    private Transform currentPoint;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnim;
    public GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Getting all components and assigning them to the variables
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        currentPoint = pointB.transform;
        playerAnim = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;

        //Enemies only move while player is alive and playing
        if (playerAnim.enabled == true)
        {

            //Enemies patrol between two points on the ground
            if (currentPoint == pointB.transform)
            {
                rb.linearVelocity = new Vector2(speed, 0);
            }
            else
            {
                rb.linearVelocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
            }
        }

        //Enemies stop moving when player dies
        else
        {
            speed = 0;
            rb.linearVelocity = new Vector2(speed, 0);
        }


        //Enemy sprite faces correct movement direction
        if (currentPoint == pointB.transform)
        {
            spriteRenderer.flipX = true;
        }

        if (currentPoint == pointA.transform)
        {
            spriteRenderer.flipX = false;
        }
    }
}
