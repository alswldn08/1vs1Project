using UnityEngine;

public class LockCameraY : MonoBehaviour
{
    public float fixedY = 5f; // 원하는 Y 위치

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = fixedY;
        transform.position = pos;
    }
}
