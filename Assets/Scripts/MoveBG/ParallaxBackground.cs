using UnityEngine;

public class ParallaxMaterialScroller : MonoBehaviour
{
    [System.Serializable]
    public class ParallaxMaterialLayer
    {
        public Material material;      // ������ ��Ƽ����
        public float parallaxFactor;   // ������ ���� (0~1)
        public Vector2 direction = Vector2.right; // �⺻ x ���� ��ũ��
        [HideInInspector]
        public Vector2 currentOffset = Vector2.zero;

        [HideInInspector]
        public string textureProperty = "_MainTex"; // �⺻��
    }

    public ParallaxMaterialLayer[] layers;
    private Transform cam;
    private Vector3 previousCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        previousCamPos = cam.position;

        // offset �ʱ�ȭ �� �ؽ�ó �Ӽ� �̸� ����
        foreach (ParallaxMaterialLayer layer in layers)
        {
            if (layer.material == null) continue;

            // URP ȣȯ�� �Ӽ� Ȯ��
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
