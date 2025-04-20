using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody2D rb;
    private Weapon weapon;

    [SerializeField]
    private float power;

    [SerializeField]
    Transform pos; // 발 중앙 기준 위치

    [SerializeField]
    float rayLength = 0.2f;

    [SerializeField]
    float footOffset = 0.2f; // 양쪽 발 간격 (왼쪽/오른쪽)

    [SerializeField]
    LayerMask isLayer;

    bool isGround;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
    }

    private void Update()
    {
        Vector2 leftRayOrigin = new Vector2(pos.position.x - footOffset, pos.position.y);
        Vector2 rightRayOrigin = new Vector2(pos.position.x + footOffset, pos.position.y);

        RaycastHit2D leftHit = Physics2D.Raycast(leftRayOrigin, Vector2.down, rayLength, isLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRayOrigin, Vector2.down, rayLength, isLayer);

        isGround = leftHit.collider != null || rightHit.collider != null;

        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, power);
        }

        // 디버그용 Ray 표시
        Debug.DrawRay(leftRayOrigin, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(rightRayOrigin, Vector2.down * rayLength, Color.red);
    }

    private void FixedUpdate()
    {
        float move = 0f;

        if (weapon != null && weapon.data.isReloading)
        {
            speed = 2f;
        }
        else
        {
            speed = 5f;
        }

        if (Input.GetKey(KeyCode.A))
        {
            move = -1f;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = 1f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        rb.velocity = new Vector2(speed * move, rb.velocity.y);
    }

    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }
}
