using Unity.VisualScripting;
using UnityEngine;

public class KillPlayerOnCollision : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    /*Update is caled once per frame*/
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            /*Destroy the player game object*/
            other.gameObject.SetActive(false);
            //Destroy(GameObject.FindGameObjectWithTag("Player"));

        }
    }
}
