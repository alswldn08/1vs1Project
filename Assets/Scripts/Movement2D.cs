using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private LayerMask collisionLayer;               //         浹 ϴ     ̾ 

    [SerializeField]
    [Range(2, 100)]
    private int horizontalRayCount = 2;     // x             ߻  ϴ             
    private float horizontalRaySpacing;     // x             ߻  ϴ                  

    [SerializeField]
    [Range(2, 100)]
    private int verticalRayCount = 2;       // y             ߻  ϴ             
    private float verticalRaySpacing;           // y             ߻  ϴ                  

    [SerializeField]
    private float moveSpeed = 5;                //  ̵   ӵ 
    [SerializeField]
    private float jumpForce = 10;               //        
    [SerializeField]
    private float lowGravity = -20;         //     Ű                           Ǵ   ߷ 
    [SerializeField]
    private float highGravity = -30;            //  Ϲ            Ǵ   ߷ 
    private float gravity = -30;                //  ߷ 
    [SerializeField]
    private int maxJumpCount = 2;           //  ִ       Ƚ  
    private int currentJumpCount = 0;       //           ִ       Ƚ  
    private Vector3 velocity;                   //  ӷ  (     *  ӵ )
    private float skinWidth = 0.015f;           //       Ʈ           İ     ҷ      

    private CapsuleCollider2D capsuleCollider2D;  //       Ʈ    ܰ    ġ  ľ     Ȱ  
    private ColliderCorner2D colliderCorner2D;  //       Ʈ    𼭸    ġ
    private CollisionChecker2D collisionChecker2D;  // 4         浹     

    public bool IsLongJump { set; get; } = false;

    private void Awake()
    {
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
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
    //    //  ߷ 
    //    velocity.y += gravity * Time.deltaTime;

    //    //           ӿ      Ǵ        ӷ 
    //    Vector3 currentVelocity = velocity * Time.deltaTime;

    //    // x  ӷ    0    ƴ             ߻     浹       ˻ 
    //    if (currentVelocity.x != 0)
    //    {
    //        RaycastsHorizontal(ref currentVelocity);
    //    }
    //    if (currentVelocity.y != 0)
    //    {
    //        RaycastsVertical(ref currentVelocity);
    //    }

    //    //       Ʈ  ̵ 
    //    transform.position += currentVelocity;
    //}

    private void OnDrawGizmos()
    {
        //  ׷                 
        Gizmos.color = Color.blue;

        for (int i = 0; i < horizontalRayCount; ++i)
        {
            Vector2 position = Vector2.up * horizontalRaySpacing * i;
            //   /         ߻    ġ          (  ġ,       )
            Gizmos.DrawSphere(colliderCorner2D.bottomLeft + position, 0.1f);
            Gizmos.DrawSphere(colliderCorner2D.bottomRight + position, 0.1f);
        }
        for (int i = 0; i < verticalRayCount; ++i)
        {
            Vector2 position = Vector2.right * verticalRaySpacing * i;
            //   / Ʒ        ߻    ġ          (  ġ,       )
            Gizmos.DrawSphere(colliderCorner2D.bottomLeft + position, 0.1f);
            Gizmos.DrawSphere(colliderCorner2D.topLeft + position, 0.1f);
        }
    }

    /// <summary>
    /// x  ̵           .  ܺο    ȣ  
    /// </summary>
    public void MoveTo(float x)
    {
        velocity.x = x * moveSpeed;
    }

    /// <summary>
    ///          .  ܺο    ȣ  
    /// </summary>
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


    /// <summary>
    /// x  ̵                 ߻  (   or   )
    /// </summary>
    private void RaycastsHorizontal(ref Vector3 velocity)
    {
        float direction = Mathf.Sign(velocity.x);               //  ̵       (  :1,   :-1)
        float distance = Mathf.Abs(velocity.x) + skinWidth; //          
        Vector2 rayPosition = Vector2.zero;                     //         ߻ Ǵ    ġ
        RaycastHit2D hit;

        for (int i = 0; i < horizontalRayCount; ++i)
        {
            rayPosition = direction == 1 ? colliderCorner2D.bottomRight : colliderCorner2D.bottomLeft;
            rayPosition += Vector2.up * horizontalRaySpacing * i;

            //       ߻ 
            hit = Physics2D.Raycast(rayPosition, Vector2.right * direction, distance, collisionLayer);

            //         ε            Ʈ          
            if (hit)
            {
                // x    ӷ                 Ʈ         Ÿ         ( Ÿ    0 ̸   ӷ  0)
                velocity.x = (hit.distance - skinWidth) * direction;
                //  ݺ          Ǳ                 ߻ Ǵ          Ÿ      
                distance = hit.distance;

                //       ̵     ⿡      left  Ǵ  right   true        ȴ 
                collisionChecker2D.left = direction == -1;
                collisionChecker2D.right = direction == 1;
            }

            Debug.DrawLine(rayPosition, rayPosition + Vector2.right * direction * distance, Color.yellow);
        }
    }

    /// <summary>
    /// y  ̵                 ߻  (   or  Ʒ )
    /// </summary>
    private void RaycastsVertical(ref Vector3 velocity)
    {
        float direction = Mathf.Sign(velocity.y);               //  ̵       (  :1,  Ʒ :-1)
        float distance = Mathf.Abs(velocity.y) + skinWidth; //          
        Vector2 rayPosition = Vector2.zero;                     //         ߻ Ǵ    ġ
        RaycastHit2D hit;

        for (int i = 0; i < verticalRayCount; ++i)
        {
            rayPosition = direction == 1 ? colliderCorner2D.topLeft : colliderCorner2D.bottomLeft;
            rayPosition += Vector2.right * (verticalRaySpacing * i + velocity.x);

            //       ߻ 
            hit = Physics2D.Raycast(rayPosition, Vector2.up * direction, distance, collisionLayer);

            //         ε            Ʈ          
            if (hit)
            {
                // y    ӷ                 Ʈ         Ÿ         ( Ÿ    0 ̸   ӷ  0)
                velocity.y = (hit.distance - skinWidth) * direction;
                //  ݺ          Ǳ                 ߻ Ǵ          Ÿ      
                distance = hit.distance;

                //       ̵     ⿡      down  Ǵ  up   true        ȴ 
                collisionChecker2D.down = direction == -1;
                collisionChecker2D.up = direction == 1;
            }

            Debug.DrawLine(rayPosition, rayPosition + Vector2.up * direction * distance, Color.yellow);
        }
    }

    /// <summary>
    ///            ߻ Ǵ                  
    /// </summary>
    private void CalculateRaySpacing()
    {
        //      Collider2D    ܰ    ġ         ޾ƿ   ,
        // skinWidth  ŭ              
        Bounds bounds = capsuleCollider2D.bounds;
        bounds.Expand(skinWidth * -2);

        // x             ߻  ϴ             
        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        // y             ߻  ϴ             
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    /// <summary>
    ///  浹     (Collider)    ܰ       ġ
    /// </summary>
    private void UpdateColliderCorner2D()
    {
        //      Collider2D    ܰ    ġ         ޾ƿ   ,
        // skinWidth  ŭ              
        Bounds bounds = capsuleCollider2D.bounds;
        bounds.Expand(skinWidth * -2);

        colliderCorner2D.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        colliderCorner2D.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        colliderCorner2D.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
    }

    private struct ColliderCorner2D
    {
        public Vector2 topLeft;     //         
        public Vector2 bottomLeft;      //       ϴ 
        public Vector2 bottomRight; //         ϴ 
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
}

