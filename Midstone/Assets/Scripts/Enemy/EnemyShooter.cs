using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooter : Enemy
{
    public Rigidbody2D projectile;
    public Transform projectileSpawnPoint;
    public float projectileForce;

    private bool Fired;

    void Start()
    {
        StartUp();
        rb = GetComponent<Rigidbody2D>();

        Fired = false;
        isGone = false;
    }

    void Update()
    {
        EUpdate();
        StartCoroutine(Fire(2));
    }

    IEnumerator Fire(float time)
    {
        if (!Fired)
        {
            Fired = true;
            yield return new WaitForSeconds(time);

            Rigidbody2D temp = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

            if (isFacingRight)
            {
                temp.AddForce(projectileSpawnPoint.right * projectileForce, ForceMode2D.Impulse);
            }
            else
            {
                temp.AddForce(-projectileSpawnPoint.right * projectileForce, ForceMode2D.Impulse);
            }
            Fired = false;
        }
    }
}
