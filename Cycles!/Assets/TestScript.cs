using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 newPos;
    private bool isJump;

    void Start()
    {
        newPos = new Vector2(2.0f, 2.0f);
        rb = GetComponent<Rigidbody2D>();
        isJump = false;
    }

    //Just hit another collider 2D
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isJump = false;
    }

   

    private void Update()
    {
        if (Input.GetKeyDown("space") && !isJump)
        {
            isJump = true;
            rb.velocity = Vector2.up * 5f;
        }
    }
}
