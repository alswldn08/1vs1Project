using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField]
    private Transform player;  // 따라갈 대상 (플레이어)

    [SerializeField]
    private Vector3 offset = new Vector3(0, 0, 0);  // 따라갈 때의 위치 오프셋

    private void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
        }
    }
}
