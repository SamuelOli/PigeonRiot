using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1;
    Rigidbody2D rb;
    public bool stop = false;
    
    Animator animator;
    SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        render = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            Move();
        }
    }

    public void Move()
    {
        stop = false;
        rb.velocity = new Vector2 (Input.GetAxis("Horizontal") * speed, Input.GetAxis("Vertical") * speed);
        if(rb.velocity.magnitude > 0)
        {
            animator.SetBool("Walk", true);
            if (rb.velocity.x > 0)
            {
                render.flipX = false;
            }
            else if(rb.velocity.x < 0)
            {
                render.flipX = true;
            }
        }
        else
        {
            animator.SetBool("Walk", false);
        }
    }

    public void Stop()
    {
        rb.velocity = new Vector2(0,0);
        stop = true;
    }
}
