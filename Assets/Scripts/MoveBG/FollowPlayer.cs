using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;        // 따라갈 플레이어
    private Vector3 offset;         // 시작 시 계산되는 오프셋

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player 트랜스폼이 설정되지 않았습니다.");
            return;
        }

        // 처음 시작할 때 오프셋 계산
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // x 좌표만 따라가고 나머지는 현재 카메라 값 유지
        Vector3 newPos = transform.position;
        newPos.x = player.position.x + offset.x;
        transform.position = newPos;
    }
}
