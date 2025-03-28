using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 6f;
    private Rigidbody2D rb;
    private Weapon weapon;

    [SerializeField]
    private float power;
    [SerializeField]
    Transform pos;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask isLayer;

    public Data data;

    bool isGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, isLayer);
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = Vector2.up * power;
        }
    }

    private void FixedUpdate()
    {
        float move = 0f;

        if (weapon.data.isReloading)
        {
            speed = 3f;
        }
        else
        {
            speed = 6f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            move = -1f;
            transform.rotation = Quaternion.Euler(0, 180, 0); // 왼쪽을 향하게 회전
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = 1f;
            transform.rotation = Quaternion.Euler(0, 0, 0); // 오른쪽을 향하게 회전
        }

        rb.velocity = new Vector2(speed * move, rb.velocity.y);
    }
}