using UnityEngine;

public class LockCameraY : MonoBehaviour
{
    public float fixedY = 5f; // ���ϴ� Y ��ġ

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.y = fixedY;
        transform.position = pos;
    }
}
