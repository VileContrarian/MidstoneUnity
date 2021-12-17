using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public Rigidbody2D rb;
    Animator anim;

    public float speed;
    float moveValue;
    public float jumpForce;

    public bool isGrounded;

    public bool isFacingRight;
    public int tick;

    public Rigidbody2D projectile;
    public Transform projectileSpawnPoint;
    public float projectileForce;

    public GameObject spawn;

    public int lives;
    void Start()
    {

        tag = "Player";
        name = "Player";

        lives = 3;

        PauseMenu.lives = lives;

        rb = GetComponent<Rigidbody2D>();

        rb.mass = 1.0f;
        rb.gravityScale = 0.5f;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.sleepMode = RigidbodySleepMode2D.NeverSleep;

        if (speed <= 0 || speed > 5.0f)
        {
            speed = 3.0f;
        }

        if (projectileForce <= 0 || projectileForce > 5.0f)
        {
            projectileForce = 2.0f;
        }

        if (jumpForce <= 0 || jumpForce > 10.0f)
        {
            jumpForce = 4.0f;
        }

        anim = GetComponent<Animator>();

        if (!anim)
        {
            Debug.LogError("Animator not found on " + name);
        }

        tick = 0;
    }

    void Update()
    {
        anim.SetBool("Grounded", false);


        if (tick >= 1 && tick <= 15)
        {
            tick++;
        }
        else
        {
            tick = 0;
        }

        moveValue = 0;

        if (rb.velocity.y == 0)
        {
            anim.SetBool("Jump", false);
            anim.SetBool("Grounded", true);
        }
        else if (rb.velocity.y > 0)
        {
            anim.SetBool("Jump", true);
            anim.SetBool("Grounded", false);
        }
        else if (rb.velocity.y < 0)
        {
            anim.SetBool("Jump", false);
        }

        Controls();

        if (rb)
        {
            rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);
        }

        if (anim)
        {
            // Activate tranisitions in Animator
            //anim.SetBool("Grounded", isGrounded);
            anim.SetFloat("Movement", Mathf.Abs(moveValue));
        }

        if ((moveValue > 0 && isFacingRight) || (moveValue < 0 && !isFacingRight))
        {
            flip();

        }

        //anim.SetBool("Dead", false);
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{

    //}

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BlockerL")
        {
            transform.position = new Vector3(11, transform.position.y, transform.position.z);
        }

        if (collision.gameObject.tag == "BlockerR")
        {
            transform.position = new Vector3(-11, transform.position.y, transform.position.z);
        }

        if (collision.gameObject.tag == "Death")
        {
            if (!anim.GetBool("Dead"))
            {
                lives--;
                PauseMenu.lives = lives;
                anim.SetBool("Dead", true);
                rb.AddForce(Vector2.down * (jumpForce + (-rb.velocity.y)), ForceMode2D.Impulse);
            }
        }

        if (collision.gameObject.tag == "End")
        {
            PauseMenu.over = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!anim.GetBool("Dead"))
            {
                lives--;
                PauseMenu.lives = lives;
                anim.SetBool("Dead", true);
                rb.AddForce(Vector2.down * (jumpForce + (-rb.velocity.y)), ForceMode2D.Impulse);
            }
        }
    }

    void flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scaleFactor = transform.localScale;

        scaleFactor.x *= -1; // or - -scaleFactor.x

        transform.localScale = scaleFactor;
    }

    void Controls()
    {
        if (!anim.GetBool("Dead"))
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Rigidbody2D temp = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

                anim.Play("Slash");
                if (isFacingRight)
                {
                    temp.AddForce(-projectileSpawnPoint.right * projectileForce, ForceMode2D.Impulse);
                }
                else
                {
                    temp.AddForce(projectileSpawnPoint.right * projectileForce, ForceMode2D.Impulse);
                }
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                if (anim.GetBool("Grounded"))
                {
                    Debug.Log("Jump");

                    //SoundManager.PlaySound("PlayerJump");
                    rb.AddForce(Vector2.up * (jumpForce + (-rb.velocity.y)), ForceMode2D.Impulse);
                }
            }


            if (Input.GetKey(KeyCode.A))
            {
                moveValue = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                moveValue = 1;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (lives != 0)
                {
                    anim.SetBool("Dead", false);
                    anim.Play("Idle");
                    transform.position = spawn.transform.position;
                }
                else
                {
                    PauseMenu.over = true;
                }
            }
        }
    }

}
