using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // 따라갈 플레이어
    private Vector3 offset;   // Y, Z는 고정할 오프셋

    void Start()
    {
        // 현재 카메라 위치와 플레이어 위치의 차이를 계산 (Y와 Z 오프셋)
        offset = transform.position - player.position;
        offset.x = 0f; // X는 따라갈 거니까 offset은 0
    }

    void LateUpdate()
    {
        // 카메라 위치를 업데이트: X는 플레이어 따라가고, Y와 Z는 고정
        transform.position = new Vector3(player.position.x, player.position.y + offset.y, player.position.z + offset.z);
    }
}
