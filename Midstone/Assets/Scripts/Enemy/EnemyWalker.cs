using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWalker : Enemy
{
    void Start()
    {
        StartUp();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!anim.GetBool("Dead"))
        {
            EUpdate();
            if (rb)
            {
                rb.velocity = new Vector2(moveValue * speed / 2, rb.velocity.y);
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (!anim.GetBool("Dead"))
        {

            if (collision.gameObject.CompareTag("Blocker"))
            {
                TurnAround();
            }
            if (collision.gameObject.tag == "Slash")
            {
                anim.SetBool("Dead", true);
                anim.Play("Die");
            }
        }
    }

    void TurnAround()
    {
        if (isFacingRight && tick == 0)
        {
            flip();
            moveValue = 1;
            tick = 1;
        }
        else if (!isFacingRight && tick == 0)
        {
            flip();
            moveValue = -1;
            tick = 1;
        }

    }
}
