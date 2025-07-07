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

    public ParallaxLayer[] layers;
    private Transform cam;
    private Vector3 previousCamPos;

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

            // 축별 패럴럭스 적용
            float moveX = delta.x * layer.parallaxFactorX;
            float moveY = delta.y * layer.parallaxFactorY;

            layer.target.position += new Vector3(moveX, moveY, 0f);
        }

        previousCamPos = cam.position;
    }
}
