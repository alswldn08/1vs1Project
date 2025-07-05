using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class Player : MonoBehaviour
{
    public static Player i { get; private set; }

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
    bool isDash;
    private int currentAnimState = -1;
    private bool isAnimLocked = false;
    private float animLockTimer = 0f;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Update()
    {
        // 애니메이션 락 타이머 감소
        if (animLockTimer > 0f)
        {
            animLockTimer -= Time.deltaTime;
            if (animLockTimer <= 0f)
            {
                isAnimLocked = false;
            }
        }

        // 이동/점프 입력은 애니메이션 3번 잠금 중일 땐 차단
        if (isAnimLocked && currentAnimState == 3)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); // 움직임 멈춤
        }
        else
        {
            // 방향 입력 처리
            float move = 0f;

            if (weapon != null && weapon.data.isReloading)
            {
                speed = 2f;
            }
            else if (weapon != null && !weapon.data.isReloading)
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

            rb.velocity = new Vector2(speed * move, rb.velocity.y);

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
        }

        // 애니메이션 락이 풀렸고 멈춰있는 상태라면 애니메이션을 0번(Idle)로 돌려줌
        if (!isDash && !weapon.data.isReloading && rb.velocity.x == 0 && isGround)
        {
            ChangeAnimation(0);
        }

        dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDash && dashCooldownTimer <= 0f)
        {
            if (playerEnergy >= 20f)
            {
                StartDash();
            }
            else
            {
                Debug.Log("스테미너 부족!");
            }
        }

        Debug.DrawRay(new Vector2(pos.position.x - footOffset, pos.position.y), Vector2.down * rayLength, Color.red);
        Debug.DrawRay(new Vector2(pos.position.x + footOffset, pos.position.y), Vector2.down * rayLength, Color.red);
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

    public void ChangeAnimation(int stateNum, float lockDuration = 0f)
    {
        if (isAnimLocked) return;

        if (currentAnimState == stateNum) return;

        animator.SetInteger("states", stateNum);
        currentAnimState = stateNum;

        if (lockDuration > 0f)
        {
            isAnimLocked = true;
            animLockTimer = lockDuration;
        }
    }

}

