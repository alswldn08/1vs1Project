using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private LayerMask collisionLayer;
    [SerializeField]
    [Range(2, 100)]
    private int horizontalRayCount = 2;         
    private float horizontalRaySpacing;             

    [SerializeField]
    [Range(2, 100)]
    private int verticalRayCount = 2;       
    private float verticalRaySpacing;        

    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float jumpForce = 10;      
    [SerializeField]
    private float lowGravity = -20;
    [SerializeField]
    private float highGravity = -30;
    private float gravity = -30;
    [SerializeField]
    private int maxJumpCount = 2;
    private int currentJumpCount = 0;
    private Vector3 velocity;
    private float skinWidth = 0.015f;
    private Weapon weapon;

    private CapsuleCollider2D capsuleCollider2D;
    private ColliderCorner2D colliderCorner2D;
    private CollisionChecker2D collisionChecker2D;

    public bool IsLongJump { set; get; } = false;

    private void Awake()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    private void Start()
    {
        weapon = FindObjectOfType<Weapon>();
    }

    private void Update()
    {
        // 중력 세기 조절만 담당
        gravity = (IsLongJump && velocity.y > 0) ? lowGravity : highGravity;
    }


    private void FixedUpdate()
    {
        // Step 1: 충돌 검사 준비
        CalculateRaySpacing();
        UpdateColliderCorner2D();
        collisionChecker2D.Reset();

        // Step 2: 중력 적용
        velocity.y += gravity * Time.fixedDeltaTime;

        // Step 3: 충돌 감지를 먼저 수행해서 바닥 체크
        Vector3 currentVelocity = velocity * Time.fixedDeltaTime;

        if (currentVelocity.x != 0)
        {
            RaycastsHorizontal(ref currentVelocity);
        }
        if (currentVelocity.y != 0)
        {
            RaycastsVertical(ref currentVelocity);
        }

        // Step 4: 충돌 체크 이후 점프 회복 처리
        if (collisionChecker2D.down && velocity.y <= 0)
        {
            currentJumpCount = maxJumpCount;
        }

        // Step 5: 충돌 시 Y속도 초기화
        if (collisionChecker2D.up || collisionChecker2D.down)
        {
            velocity.y = 0;
        }

        // Step 6: 실제 이동
        transform.position += currentVelocity;
    }



    //private void UpdateMovement()
    //{
    //    velocity.y += gravity * Time.deltaTime;

    //    Vector3 currentVelocity = velocity * Time.deltaTime;

    //    if (currentVelocity.x != 0)
    //    {
    //        RaycastsHorizontal(ref currentVelocity);
    //    }
    //    if (currentVelocity.y != 0)
    //    {
    //        RaycastsVertical(ref currentVelocity);
    //    }
    //    transform.position += currentVelocity;
    //}

    //private void OnDrawGizmos()
    //{         
    //    Gizmos.color = Color.blue;

    //    for (int i = 0; i < horizontalRayCount; ++i)
    //    {
    //        Vector2 position = Vector2.up * horizontalRaySpacing * i;
    //        Gizmos.DrawSphere(colliderCorner2D.bottomLeft + position, 0.1f);
    //        Gizmos.DrawSphere(colliderCorner2D.bottomRight + position, 0.1f);
    //    }
    //    for (int i = 0; i < verticalRayCount; ++i)
    //    {
    //        Vector2 position = Vector2.right * verticalRaySpacing * i;
    //        Gizmos.DrawSphere(colliderCorner2D.bottomLeft + position, 0.1f);
    //        Gizmos.DrawSphere(colliderCorner2D.topLeft + position, 0.1f);
    //    }
    //}
    public void MoveTo(float x)
    {
        velocity.x = x * moveSpeed;
    }

    public bool JumpTo()
    {
        if (currentJumpCount > 0)
        {
            velocity.y = jumpForce;
            currentJumpCount--;
            return true;
        }
        return false;
    }

    private void RaycastsHorizontal(ref Vector3 velocity)
    {
        float direction = Mathf.Sign(velocity.x);
        float distance = Mathf.Abs(velocity.x) + skinWidth;       
        Vector2 rayPosition = Vector2.zero;
        RaycastHit2D hit;

        for (int i = 0; i < horizontalRayCount; ++i)
        {
            rayPosition = direction == 1 ? colliderCorner2D.bottomRight : colliderCorner2D.bottomLeft;
            rayPosition += Vector2.up * horizontalRaySpacing * i;

            hit = Physics2D.Raycast(rayPosition, Vector2.right * direction, distance, collisionLayer);
  
            if (hit)
            {
                velocity.x = (hit.distance - skinWidth) * direction; 
                distance = hit.distance;

                collisionChecker2D.left = direction == -1;
                collisionChecker2D.right = direction == 1;
            }

            Debug.DrawLine(rayPosition, rayPosition + Vector2.right * direction * distance, Color.yellow);
        }
    }

    private void RaycastsVertical(ref Vector3 velocity)
    {
        float direction = Mathf.Sign(velocity.y);
        float distance = Mathf.Abs(velocity.y) + skinWidth;
        Vector2 rayPosition = Vector2.zero;
        RaycastHit2D hit;

        for (int i = 0; i < verticalRayCount; ++i)
        {
            rayPosition = direction == 1 ? colliderCorner2D.topLeft : colliderCorner2D.bottomLeft;
            rayPosition += Vector2.right * (verticalRaySpacing * i + velocity.x);

            hit = Physics2D.Raycast(rayPosition, Vector2.up * direction, distance, collisionLayer);

            if (hit)
            {
                velocity.y = (hit.distance - skinWidth) * direction;  
                distance = hit.distance;

                collisionChecker2D.down = direction == -1;
                collisionChecker2D.up = direction == 1;
            }

            Debug.DrawLine(rayPosition, rayPosition + Vector2.up * direction * distance, Color.yellow);
        }
    }

    private void CalculateRaySpacing()
    {
        Bounds bounds = capsuleCollider2D.bounds;
        bounds.Expand(skinWidth * -2);
       
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    private void UpdateColliderCorner2D()
    {

        Bounds bounds = capsuleCollider2D.bounds;
        bounds.Expand(skinWidth * -2);

        colliderCorner2D.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        colliderCorner2D.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        colliderCorner2D.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }

    private struct ColliderCorner2D
    {
        public Vector2 topLeft;      
        public Vector2 bottomLeft;
        public Vector2 bottomRight;
    }

    public struct CollisionChecker2D
    {
        public bool up;
        public bool down;
        public bool left;
        public bool right;

        public void Reset()
        {
            up = false;
            down = false;
            left = false;
            right = false;
        }
    }

    public void SetWeapon(Weapon newWeapon)
    {
        weapon = newWeapon;
    }
}

