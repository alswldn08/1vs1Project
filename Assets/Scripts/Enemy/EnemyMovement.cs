using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float jumpForce = 7f;
    public float wallCheckWidthDistance = 0.3f;
    public float wallCheckHeightDistance = 0.3f;
    public float groundCheckDistance = 0.1f;
    public float footOffset = 0.2f;
    public LayerMask groundLayer;

    public float leftBoundaryX;
    public float rightBoundaryX;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool movingRight = true;
    public float patrolDistance = 3f;
    private float initialX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        initialX = transform.position.x;
        leftBoundaryX = initialX - patrolDistance / 2f;
        rightBoundaryX = initialX + patrolDistance / 2f;
    }

    private void FixedUpdate()
    {
        // �̵�
        float direction = movingRight ? 1f : -1f;
        rb.velocity = new Vector2(direction * moveSpeed, rb.velocity.y);

        // ��������Ʈ ����
        spriteRenderer.flipX = !movingRight;

        // ��� üũ (�պ�)
        if (movingRight && transform.position.x >= rightBoundaryX)
            movingRight = false;
        else if (!movingRight && transform.position.x <= leftBoundaryX)
            movingRight = true;

        // �� ����
        Vector2 wallCheckOrigin = new Vector2(transform.position.x + direction * wallCheckWidthDistance, transform.position.y + wallCheckHeightDistance);
        bool wallDetected = Physics2D.Raycast(wallCheckOrigin, Vector2.right * direction, 0.1f, groundLayer);

        // �ٴ� ���� (���)
        Vector2 leftFoot = new Vector2(transform.position.x - footOffset, transform.position.y);
        Vector2 rightFoot = new Vector2(transform.position.x + footOffset, transform.position.y);
        bool leftGrounded = Physics2D.Raycast(leftFoot, Vector2.down, groundCheckDistance, groundLayer);
        bool rightGrounded = Physics2D.Raycast(rightFoot, Vector2.down, groundCheckDistance, groundLayer);
        bool isGrounded = leftGrounded || rightGrounded;

        // ����
        if (wallDetected && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // ����׿� ����
        Debug.DrawRay(wallCheckOrigin, Vector2.right * direction * 0.1f, Color.red);
        Debug.DrawRay(leftFoot, Vector2.down * groundCheckDistance, Color.green);
        Debug.DrawRay(rightFoot, Vector2.down * groundCheckDistance, Color.green);
    }

    void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        float direction = movingRight ? 1f : -1f;
        Vector2 wallWidthCheckOrigin = new Vector2(transform.position.x + direction * wallCheckWidthDistance, transform.position.y);
        Vector2 wallHeightCheckOrigin = new Vector2(transform.position.x + direction * wallCheckWidthDistance, transform.position.y + wallCheckHeightDistance);
        Gizmos.DrawLine(wallWidthCheckOrigin, wallWidthCheckOrigin + Vector2.right * direction * 0.1f);
        Gizmos.DrawLine(wallHeightCheckOrigin, wallHeightCheckOrigin + Vector2.right * direction * 0.1f);

        Gizmos.color = Color.green;
        Vector2 leftFoot = new Vector2(transform.position.x - footOffset, transform.position.y);
        Vector2 rightFoot = new Vector2(transform.position.x + footOffset, transform.position.y);
        Gizmos.DrawLine(leftFoot, leftFoot + Vector2.down * groundCheckDistance);
        Gizmos.DrawLine(rightFoot, rightFoot + Vector2.down * groundCheckDistance);

        // ��� ��ġ ǥ��
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector3(leftBoundaryX, transform.position.y - 1f, 0), new Vector3(leftBoundaryX, transform.position.y + 1f, 0));
        Gizmos.DrawLine(new Vector3(rightBoundaryX, transform.position.y - 1f, 0), new Vector3(rightBoundaryX, transform.position.y + 1f, 0));
    }
}
