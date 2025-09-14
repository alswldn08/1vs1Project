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
    /// ������ �ؽ�Ʈ ����
    /// </summary>
    /// <param name="worldPos">�÷��̾�/�� ���� ��ġ</param>
    /// <param name="amount">������ �Ǵ� ȸ����</param>
    /// <param name="isHeal">ȸ���̸� true</param>
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
