using UnityEngine;
using System.Collections;
public class CameraFollow : MonoBehaviour
{
    public Transform player;     // 따라갈 플레이어
    public float cameraDelay = 0.5f;
    public Vector3 offset;       // 유지할 오프셋 (인스펙터에서 설정 가능)


    private void Update()
    {
        StartCoroutine(DelayFollow());
    }

    void LateUpdate()
    {
        // 플레이어 위치 + 오프셋 만큼 카메라 위치를 이동
        transform.position = player.position + offset;
    }

    IEnumerator DelayFollow()
    {
        yield return new WaitForSeconds(cameraDelay);
    }
}
