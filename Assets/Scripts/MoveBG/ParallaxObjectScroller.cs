using UnityEngine;

public class ParallaxObjectScroller : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxLayer
    {
        public Transform target;             // 움직일 배경 오브젝트
        public float parallaxFactorX = 0.5f; // X축 패럴럭스 정도
        public float parallaxFactorY = 0.5f; // Y축 패럴럭스 정도
    }

    public ParallaxLayer[] layers;           // 레이어 배열
    private Transform cam;                   // 메인 카메라
    private Vector3 previousCamPos;          // 이전 프레임 카메라 위치

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;
    }

    void FixedUpdate()
    {
        Vector3 delta = cam.position - previousCamPos;

        foreach (ParallaxLayer layer in layers)
        {
            if (layer.target == null) continue;

            // 축별로 다르게 패럴럭스 적용
            float moveX = delta.x * layer.parallaxFactorX;
            float moveY = delta.y * layer.parallaxFactorY;

            layer.target.position += new Vector3(moveX, moveY, 0f);
        }

        previousCamPos = cam.position;
    }
}
