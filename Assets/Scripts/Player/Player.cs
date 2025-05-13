using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region 레퍼런스
    private Rigidbody2D rb;
    private Weapon weapon;
    private PlayerEnergySlider playerEnergySlider;
    #endregion

    [SerializeField]
    private float power;

    [SerializeField]
    Transform pos;

    [SerializeField]
    float rayLength = 0.2f;

    [SerializeField]
    float footOffset = 0.2f;

    [SerializeField]
    LayerMask isLayer;

    public float speed = 5f;

    bool isGround;
    bool isFacingRight = true;

    [SerializeField]
    float dashForce = 10f;
    bool isDashing = false;
    float dashTime = 0.1f; // 대쉬 지속 시간
    float dashTimer = 0f;

    public float dashCooldown = 1f;  // 추가된 쿨타임 변수
    float dashCooldownTimer = 0f;

    public float playerHP = 100f;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
        playerEnergySlider = GetComponent<PlayerEnergySlider>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Update()
    {
        // 방향 설정
        if (Input.GetKey(KeyCode.A))
        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        dashCooldownTimer -= Time.deltaTime;

        // 대쉬 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && dashCooldownTimer <= 0f)
        {
            if(playerEnergySlider.energy.value >= 20f)
            {
                StartDash();
            }
            else
            {
                Debug.Log("스테미너 부족!");
            }
        }

        // 점프 처리
        Vector2 leftRayOrigin = new Vector2(pos.position.x - footOffset, pos.position.y);
        Vector2 rightRayOrigin = new Vector2(pos.position.x + footOffset, pos.position.y);

        RaycastHit2D leftHit = Physics2D.Raycast(leftRayOrigin, Vector2.down, rayLength, isLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRayOrigin, Vector2.down, rayLength, isLayer);

        isGround = leftHit.collider != null || rightHit.collider != null;

        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, power);
        }

        Debug.DrawRay(leftRayOrigin, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(rightRayOrigin, Vector2.down * rayLength, Color.red);
    }

    private void FixedUpdate()
    {
        if (isDashing)
        {
            float dashDir = isFacingRight ? 1f : -1f;
            rb.velocity = new Vector2(dashDir * dashForce, 0f); // y를 0으로 고정해서 위로 튀는 걸 방지
            dashTimer -= Time.fixedDeltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
            return; // 대쉬 중이면 일반 이동 무시
        }

        if(playerHP <= 0f)
        {
            GameManager.i.GameOver();
        }

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
        }
        else if (Input.GetKey(KeyCode.D))
        {
            move = 1f;
        }

        rb.velocity = new Vector2(speed * move, rb.velocity.y);
    }

    private void StartDash()
    {
        isDashing = true;
        dashTimer = dashTime;
        dashCooldownTimer = dashCooldown; // 쿨타임 초기화
        playerEnergySlider.energy.value -= 20f;
    }

    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BodyDamageEnemy"))
        {
            playerHP -= 10f;
        }
    }
}

