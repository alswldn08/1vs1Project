using UnityEngine;

public class ParallaxMaterialScroller : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxMaterialLayer
    {
        public Material material;      // 적용할 머티리얼
        public float parallaxFactor;   // 움직임 비율 (0~1)
        public Vector2 direction = Vector2.right; // 기본 x 방향 스크롤
        [HideInInspector]
        public Vector2 currentOffset = Vector2.zero;

        [HideInInspector]
        public string textureProperty = "_MainTex"; // 기본값
    }

    public ParallaxMaterialLayer[] layers;
    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;

        // offset 초기화 및 텍스처 속성 이름 결정
        foreach (ParallaxMaterialLayer layer in layers)
        {
            if (layer.material == null) continue;

            // URP 호환용 속성 확인
            if (layer.material.HasProperty("_BaseMap"))
                layer.textureProperty = "_BaseMap";
            else
                layer.textureProperty = "_MainTex";

            layer.currentOffset = Vector2.zero;
            layer.material.SetTextureOffset(layer.textureProperty, Vector2.zero);
        }
    }

    void LateUpdate()
    {
        Vector3 delta = cam.position - previousCamPos;

        foreach (ParallaxMaterialLayer layer in layers)
        {
            if (layer.material == null) continue;

            float offsetX = delta.x * layer.parallaxFactor;
            float offsetY = delta.y * layer.parallaxFactor;

            layer.currentOffset += new Vector2(offsetX, offsetY);
            layer.material.SetTextureOffset(layer.textureProperty, layer.currentOffset);
        }

        previousCamPos = cam.position;
    }
}
