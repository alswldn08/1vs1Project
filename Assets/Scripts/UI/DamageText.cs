using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DamageText : MonoBehaviour
{
    private Text text;
    private float moveSpeed = 50f;  // 위로 올라가는 속도
    private float duration = 1f;    // 표시 시간
    private float elapsed = 0f;

    private Color startColor;

    private void Awake()
    {
        text = GetComponent<Text>();
        startColor = text.color;
    }

    /// <summary>
    /// 텍스트 내용과 회복 여부 설정
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

        // 위로 이동
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // 점점 투명하게
        float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
        text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        // 시간 종료 시 삭제
        if (elapsed >= duration)
        {
            Destroy(gameObject);
        }
    }
}
