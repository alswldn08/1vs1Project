using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHP : MonoBehaviour
{
    public float hp = 100f;
    public Slider hpSlider;
    public GameObject bossUI;
    private SkillEmission skillEmission;

    private bool isDead = false;

    private void Awake()
    {
        hp = 100f;

        if (hpSlider != null)
        {
            hpSlider.maxValue = hp;
            hpSlider.value = hp;
        }

        if (bossUI != null)
            bossUI.SetActive(false); // ���� �� ����

        skillEmission = GetComponentInChildren<SkillEmission>();
    }

    private void Update()
    {
        if (isDead) return;

        if (hp <= 0f)
        {
            hp = 0f;

            if (hpSlider != null)
                hpSlider.value = hp;

            DestroyBoss();
            isDead = true;
        }
        else
        {
            if (hpSlider != null)
                hpSlider.value = hp;
        }
    }

    public void DestroyBoss()
    {
        Debug.Log("Boss defeated!");

        StartCoroutine(DisableUIAndBossAfterDelay(4f));
    }

    private IEnumerator DisableUIAndBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (bossUI != null)
            bossUI.SetActive(false);

        gameObject.SetActive(false);
        skillEmission.StopSkill();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Bullet"))
        {
            hp -= 10f;
            Destroy(collision.gameObject);
            Debug.Log("Boss hit! HP = " + hp);
        }
    }
}
