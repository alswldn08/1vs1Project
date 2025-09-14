using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossHP : MonoBehaviour
{
    public float hp = 100f;
    public Slider hpSlider;
    public GameObject bossUI;
    private SkillEmission skillEmission;
    private SpriteRenderer spriteRenderer;

    private bool isDead = false;

    // 흔들기용 변수
    private Vector3 originalPos;
    private Coroutine shakeCoroutine;

    private void Awake()
    {
        if (bossUI != null)
            bossUI.SetActive(false); // 초기 UI 비활성화

        skillEmission = GetComponentInChildren<SkillEmission>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalPos = transform.localPosition; // 흔들기 기준 위치
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

    public void SetUI()
    {
        hp = 100f;

        if (hpSlider != null)
        {
            hpSlider.maxValue = hp;
            hpSlider.value = hp;
        }
    }

    public void DestroyBoss()
    {
        Debug.Log("Boss defeated!");
        StartCoroutine(DisableUIAndBossAfterDelay(2f));
    }

    private IEnumerator DisableUIAndBossAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (bossUI != null)
            bossUI.SetActive(false);

        gameObject.SetActive(false);
        skillEmission.StopSkill();
    }

    private IEnumerator DamageEffect(Color dam)
    {
        spriteRenderer.color = dam;

        // 데미지 입자 혹은 화면 흔들림과 동시에 흔들기
        StartShake(0.2f, 0.2f, 0.1f);

        yield return new WaitForSeconds(0.1f);

        spriteRenderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isDead) return;

        if (collision.CompareTag("Bullet"))
        {
            hp -= 10f;
            Color damaged = new Color(255f / 255f, 125f / 255f, 125f / 255f);
            StartCoroutine(DamageEffect(damaged));
            Destroy(collision.gameObject);
            SoundManager.i.PlayEffect(7);
            Debug.Log("Boss hit! HP = " + hp);
        }
    }

    // ----------------- 흔들기 기능 -----------------
    public void StartShake(float amplitude = 0.2f, float frequency = 0.2f, float duration = 0.1f)
    {
        if (shakeCoroutine != null)
            StopCoroutine(shakeCoroutine);

        shakeCoroutine = StartCoroutine(ShakeRoutine(amplitude, frequency, duration));
    }

    private IEnumerator ShakeRoutine(float amplitude, float frequency, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float x = (Mathf.PerlinNoise(Time.time * frequency, 0f) - 0.5f) * 2f * amplitude;
            float y = (Mathf.PerlinNoise(0f, Time.time * frequency) - 0.5f) * 2f * amplitude;

            transform.localPosition = originalPos + new Vector3(x, y, 0f);

            yield return null;
        }

        transform.localPosition = originalPos;
        shakeCoroutine = null;
    }
}
