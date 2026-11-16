using TMPro;
using UnityEngine;

public class LoseText : MonoBehaviour
{
    public GameObject Player;
    private Animator playerAnim;
    private TextMeshPro tmp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        playerAnim = Player.GetComponent<Animator>();
        tmp = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAnim.enabled == false)
        {
            tmp.enabled = true;
        }
    }
}

