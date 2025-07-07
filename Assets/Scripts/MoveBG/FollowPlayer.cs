using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player;        // 따라갈 대상
    private Vector3 offset;         // 초기 오프셋

    void Start()
    {
        if (player == null) return;

        // 시작 시 오프셋 계산
        offset = transform.position - player.position;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // x 좌표만 따라감
        Vector3 newPos = transform.position;
        newPos.x = player.position.x + offset.x;
        transform.position = newPos;
    }
}
