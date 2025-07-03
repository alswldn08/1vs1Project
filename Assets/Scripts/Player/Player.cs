using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour
{
    #region 레퍼런스
    private Rigidbody2D rb;
    private Weapon weapon;
    #endregion
    [Header("Move")]
    [SerializeField]bool direction = true;
    [SerializeField] private float speed = 5f;

    [Header("Jump")]
    [SerializeField] bool isGround;
    [SerializeField] private Transform pos;
    [SerializeField] private LayerMask isLayer;
    [SerializeField] private float power;
    [SerializeField] private float rayLength = 0.2f;
    [SerializeField] private float footOffset = 0.2f;

    [Header("Dash")]
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashTimer = 0f;
    [SerializeField]private float dashCooldown = 1f;  // 추가된 쿨타임 변수
    [SerializeField] private float dashCooldownTimer = 0f;

    [Header("Interface")]
    public float playerHp = 100f;
    public float playerEnergy = 100f;

    [Header("Animation")]
    public Animator animator;
    bool isMove;
    bool isJump;
    bool isAttack;
    bool isDash;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Update()
    {
        // 방향 설정
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
            direction = false;
            move = -1f;
            transform.rotation = Quaternion.Euler(0, 180, 0);
            ChangeAnimation(1);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            direction = true;
            move = 1f;
            transform.rotation = Quaternion.Euler(0, 0, 0);
            ChangeAnimation(1);
        }
        else
        {
            ChangeAnimation(0);
        }

            rb.velocity = new Vector2(speed * move, rb.velocity.y);

        dashCooldownTimer -= Time.deltaTime;

        // 대쉬 입력 처리
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDash && dashCooldownTimer <= 0f)
        {
            if(playerEnergy >= 20f)
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
            ChangeAnimation(2);
        }

        Debug.DrawRay(leftRayOrigin, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(rightRayOrigin, Vector2.down * rayLength, Color.red);
    }

    private void FixedUpdate()
    {
        if (isDash)
        {
            float dashDir = direction ? 1f : -1f;
            rb.velocity = new Vector2(dashDir * dashForce, 0f); // y를 0으로 고정해서 위로 튀는 걸 방지
            dashTimer -= Time.fixedDeltaTime;

            if (dashTimer <= 0f)
            {
                isDash = false;
            }
            return; // 대쉬 중이면 일반 이동 무시
        }
    }

    private void StartDash()
    {
        isDash = true;
        dashTimer = dashTime;
        dashCooldownTimer = dashCooldown; // 쿨타임 초기화
        playerEnergy -= 20f;
    }

    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BodyDamageEnemy"))
        {
            playerHp -= 10f;
        }
    }

    public void ChangeAnimation(int stateNum)
    {
        animator.SetInteger("states", stateNum);
    }
}

