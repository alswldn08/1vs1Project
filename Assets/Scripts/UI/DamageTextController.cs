using UnityEngine;

public class DamageTextController : MonoBehaviour
{
    private static DamageTextController _instance = null;

    public static DamageTextController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DamageTextController>();
                if (_instance == null)
                {
                    Debug.LogError("There's no active DamageTextController Object");
                }
            }
            return _instance;
        }
    }

    public Canvas canvas;
    public GameObject dmgTxtPrefab;

    /// <summary>
    /// 데미지 텍스트 생성
    /// </summary>
    /// <param name="worldPos">플레이어/적 월드 위치</param>
    /// <param name="amount">데미지 또는 회복량</param>
    /// <param name="isHeal">회복이면 true</param>
    public void CreateDamageText(Vector3 worldPos, int amount, bool isHeal = false)
    {
        if (dmgTxtPrefab == null || canvas == null) return;

        GameObject dmgTextObj = Instantiate(dmgTxtPrefab, canvas.transform);
        dmgTextObj.transform.position = Camera.main.WorldToScreenPoint(worldPos);

        DamageText dmgText = dmgTextObj.GetComponent<DamageText>();
        if (dmgText != null)
        {
            dmgText.SetText(amount, isHeal);
        }
    }
}
