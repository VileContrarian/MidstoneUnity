using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime;
    void Start()
    {
        if (lifeTime <= 0f || lifeTime > 5f)
        {
            lifeTime = 2f;
        }
        Destroy(this.gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            Destroy(this.gameObject);
        }

    }
}
