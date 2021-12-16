using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;

    protected Animator anim;

    public bool isFacingRight;
    public float moveValue;
    public int tick;

    public bool isGone;

    public void StartUp()
    {
        tag = "Enemy";
        name = "Enemy";

        isGone = false;

        rb = GetComponent<Rigidbody2D>();

        rb.mass = 1.0f;
        rb.gravityScale = 0.25f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (speed <= 0 || speed > 5.0f)
        {
            speed = 5.0f;
            Debug.LogWarning("Speed not set on " + name + ". Defaulting to " + speed);
        }

        anim = GetComponent<Animator>();

        if (!anim)
        {
            Debug.LogError("Animator not found on " + name);
        }

        tick = 0;
    }

    public void EUpdate()
    {
        Gone(isGone);
        if (tick >= 1 && tick <= 15)
        {
            tick++;
        }
        else
        {
            tick = 0;
        }

        if ((rb.velocity.x > 0 && isFacingRight) || (rb.velocity.x < 0 && !isFacingRight))
        {
            flip();
        }
    }

    public void flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scaleFactor = transform.localScale;

        scaleFactor.x *= -1; // or - -scaleFactor.x

        transform.localScale = scaleFactor;
    }

    void Gone(bool isGone)
    {
        if (isGone)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (!anim.GetBool("Dead"))
        {
            if (collision.gameObject.tag == "Slash")
            {
                tag = "Untagged";
                anim.SetBool("Dead", true);
                anim.Play("Die");
            }
        }
    }
}

