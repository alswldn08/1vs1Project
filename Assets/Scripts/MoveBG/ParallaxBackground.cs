using UnityEngine;

public class ParallaxMaterialScroller : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxMaterialLayer
    {
        public Material material;                   // 스크롤 대상 머티리얼
        public float parallaxFactor;                // 패럴럭스 강도 (0~1)
        public Vector2 direction = Vector2.right;   // 스크롤 방향
        [HideInInspector] public Vector2 currentOffset = Vector2.zero;
        [HideInInspector] public string textureProperty = "_MainTex"; // 텍스처 속성명
    }

    public ParallaxMaterialLayer[] layers;
    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;

        foreach (ParallaxMaterialLayer layer in layers)
        {
            if (layer.material == null) continue;

            // URP 호환 처리
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
