using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : MonoBehaviour
{
    private Text text;
    private float moveSpeed = 50f;  // ���� �ö󰡴� �ӵ�
    private float duration = 1f;    // ǥ�� �ð�
    private float elapsed = 0f;

    private Color startColor;

    private void Awake()
    {
        text = GetComponent<Text>();
        startColor = text.color;
    }

    /// <summary>
    /// �ؽ�Ʈ ����� ȸ�� ���� ����
    /// </summary>
    public void SetText(int amount, bool isHeal)
    {
        text.text = amount.ToString();
        text.color = isHeal ? Color.green : Color.red;
        startColor = text.color;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;

        // ���� �̵�
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // ���� �����ϰ�
        float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
        text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        // �ð� ���� �� ����
        if (elapsed >= duration)
        {
            Destroy(gameObject);
        }
    }
}
