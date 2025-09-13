using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player i { get; private set; }

    #region 레퍼런스
    private Rigidbody2D rb;
    private Weapon weapon;
    #endregion

    [Header("Move")]
    [SerializeField] private bool direction = true;
    [SerializeField] private float speed = 5f;

    [Header("Jump")]
    [SerializeField] private bool isGround;
    [SerializeField] private Transform pos;
    [SerializeField] private LayerMask isLayer;
    [SerializeField] private float power;
    [SerializeField] private float rayLength = 0.2f;
    [SerializeField] private float footOffset = 0.2f;

    [Header("Dash")]
    [SerializeField] private float dashForce = 10f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashTimer = 0f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private float dashCooldownTimer = 0f;

    [Header("Interface")]
    public float playerHp = 100f;
    public float playerEnergy = 100f;

    [Header("Animation")]
    public Animator animator;
    private bool isDash;
    private int currentAnimState = -1;
    private bool isAnimLocked = false;
    private float animLockTimer = 0f;

    private bool isJumping = false;



    private void Awake()
    {
        if (i == null)
            i = this;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();
        animator = GetComponent<Animator>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    private void Update()
    {
        if(playerHp <= 0f)
        {
            SoundManager.i.PlayPlayerEffect(2);
            GameManager.i.GameOver();
        }
        // 애니메이션 락 타이머 갱신
        if (isAnimLocked)
        {
            animLockTimer -= Time.deltaTime;
            if (animLockTimer <= 0f)
            {
                isAnimLocked = false;
            }
        }

        // 바닥 체크 (좌우 발끝 기준으로 Ray 발사)
        bool wasGround = isGround;
        Vector2 leftRayOrigin = new Vector2(pos.position.x - footOffset, pos.position.y);
        Vector2 rightRayOrigin = new Vector2(pos.position.x + footOffset, pos.position.y);
        RaycastHit2D leftHit = Physics2D.Raycast(leftRayOrigin, Vector2.down, rayLength, isLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightRayOrigin, Vector2.down, rayLength, isLayer);
        isGround = leftHit.collider != null || rightHit.collider != null;

        // 착지 시 점프 상태 초기화 및 애니메이션 전환
        if (isJumping && isGround && !wasGround)
        {
            isJumping = false;
            isAnimLocked = false;
            animLockTimer = 0f;
            animator.SetInteger("states", 1);
            currentAnimState = 1;
        }

        // 대시 입력
        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDash && dashCooldownTimer <= 0f)
        {
            if (playerEnergy >= 20f)
            {
                StartDash();
                return;
            }
        }

        dashCooldownTimer -= Time.deltaTime;

        // 점프 입력
        if (isGround && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, power);
            isJumping = true;
            ChangeAnimation(2, 0f, true); // 점프 애니메이션
            return;
        }

        if (isDash) return;

        // 공격 애니메이션 도중 이동 제한
        if (isAnimLocked && currentAnimState == 3)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        else
        {
            float move = 0f;

            speed = (weapon != null && weapon.data.isReloading) ? 2f : 5f;

            if (Input.GetKey(KeyCode.A))
            {
                direction = false;
                move = -1f;
                transform.rotation = Quaternion.Euler(0, 180, 0);
                ChangeAnimation(1); // 걷기
            }
            else if (Input.GetKey(KeyCode.D))
            {
                direction = true;
                move = 1f;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                ChangeAnimation(1); // 걷기
            }

            rb.velocity = new Vector2(speed * move, rb.velocity.y);

            if (move == 0f && rb.velocity.y == 0 && isGround && !isJumping)
            {
                ChangeAnimation(0); // 정지
            }
        }

        Debug.DrawRay(leftRayOrigin, Vector2.down * rayLength, Color.red);
        Debug.DrawRay(rightRayOrigin, Vector2.down * rayLength, Color.red);
    }

    private void FixedUpdate()
    {
        // 대시 중 물리 이동
        if (isDash)
        {
            float dashDir = direction ? 1f : -1f;
            rb.velocity = new Vector2(dashDir * dashForce, 0f);
            dashTimer -= Time.fixedDeltaTime;

            if (dashTimer <= 0f)
            {
                isDash = false;
            }
        }
    }

    private void StartDash()
    {
        isDash = true;
        dashTimer = dashTime;
        dashCooldownTimer = dashCooldown;
        playerEnergy -= 20f;
        ChangeAnimation(4, dashTime, true); // 대시 애니메이션
    }

    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BodyDamageEnemy")) // 단순 충돌시 데미지 주는 몬스터
        {
            SoundManager.i.PlayPlayerEffect(1);
            playerHp -= 10f;
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BossBullet")) // 보스의 발사체 공격
        {
            SoundManager.i.PlayPlayerEffect(1);
            playerHp -= 2f;
        }
    }

    public void ChangeAnimation(int stateNum, float lockDuration = 0f, bool ignoreLock = false)
    {
        if (isAnimLocked && !ignoreLock) return;
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
