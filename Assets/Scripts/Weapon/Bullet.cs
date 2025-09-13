using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;
    private float direction;
    public GameObject hitEffect;

    public void SetDirection(float dir)
    {
        direction = dir;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = new Vector2(direction * bulletSpeed, 0);
        }

        Destroy(gameObject, 3f);
    }

    public void HitBullet()
    {
        Destroy(gameObject);
        Instantiate(hitEffect, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            HitBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletDestroyer"))
        {
            HitBullet();
        }
    }
}
