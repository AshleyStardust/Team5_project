using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class EnemyController : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;

    private Transform currentPoint;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator playerAnim;
    public float speed;
    public GameObject Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
        if (playerAnim.enabled == true)
        {
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
        else
        {
            speed = 0;
            rb.linearVelocity = new Vector2(speed, 0);
        }

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
