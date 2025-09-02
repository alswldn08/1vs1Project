using UnityEngine;

public class BossMissile : MonoBehaviour
{
    public float speed = 6f;            // 미사일 속도
    public float rotateSpeed = 180f;    // 초당 회전 속도
    public float homingDelay = 0.5f;    // 유도 시작까지 딜레이

    private Rigidbody2D rb;
    private Transform target;
    private float timer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
            Debug.LogError("Rigidbody2D가 없습니다!");
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    private void FixedUpdate()
    {
        if (rb == null) return;

        timer += Time.fixedDeltaTime;

        // 유도 전 직진
        if (timer < homingDelay || target == null)
        {
            rb.velocity = transform.up * speed;
            return;
        }

        // === 유도 시작 ===
        Vector2 direction = ((Vector2)target.position - rb.position).normalized;

        // 현재 방향과 목표 방향 사이 각도
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        // Rigidbody2D를 이용한 부드러운 회전
        float newAngle = Mathf.MoveTowardsAngle(rb.rotation, targetAngle, rotateSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(newAngle);

        // 항상 transform.up 방향으로 속도 적용
        rb.velocity = transform.up * speed;
    }
}
