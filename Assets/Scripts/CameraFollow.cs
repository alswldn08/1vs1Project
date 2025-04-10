using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;     // 따라갈 플레이어
    public Vector3 offset;       // 유지할 오프셋 (인스펙터에서 설정 가능)

    void LateUpdate()
    {
        // 플레이어 위치 + 오프셋 만큼 카메라 위치를 이동
        transform.position = player.position + offset;
    }
}
