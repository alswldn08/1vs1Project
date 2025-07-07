using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private BossHP bossHP;
    public GameObject[] attackObjects;
    [SerializeField] private float warningTime = 1f;
    [SerializeField] private float hitDuration = 1f;
    [SerializeField] private float delayBetweenAttacks = 1f; // 공격 간 딜레이 시간
    [SerializeField] private int simultaneousCount = 3; // 동시에 공격할 오브젝트 수
    public GameObject potal;
    public bool isLooping = true;

    private void Start()
    {
        bossHP = FindObjectOfType<BossHP>();
    }

    private void Update()
    {
        if (bossHP.hpSlider.gameObject.activeSelf && bossHP.hpSlider.value <= 0)
        {
            StopAllCoroutines(); // 보스가 죽으면 공격 중지
            potal.SetActive(true); 
        }
    }

    public void StartBossAttack()
    {
        bossHP.bossUI.SetActive(true);
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (isLooping)
        {
            // 랜덤으로 simultaneousCount 개의 공격 오브젝트 선택
            List<GameObject> selected = new List<GameObject>();
            while (selected.Count < simultaneousCount)
            {
                GameObject candidate = attackObjects[Random.Range(0, attackObjects.Length)];
                if (!selected.Contains(candidate))
                    selected.Add(candidate);
            }

            // 공격 실행
            List<Coroutine> runningCoroutines = new List<Coroutine>();
            foreach (var obj in selected)
            {
                runningCoroutines.Add(StartCoroutine(AttackSequence(obj)));
            }

            // 모든 공격이 끝날 때까지 대기
            foreach (var co in runningCoroutines)
            {
                yield return co;
            }

            // 다음 공격 전 딜레이
            yield return new WaitForSeconds(delayBetweenAttacks);
        }
    }

    private IEnumerator AttackSequence(GameObject obj)
    {
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        Collider2D col = obj.GetComponent<Collider2D>();
        Color originalColor = sr.color;

        GameObject childEffect = obj.transform.childCount > 0 ? obj.transform.GetChild(0).gameObject : null;

        obj.SetActive(true);
        if (col != null) col.enabled = false;
        if (childEffect != null) childEffect.SetActive(false);

        // 경고 이펙트
        yield return StartCoroutine(BlinkEffect(sr, warningTime));

        // 색상 변경 (공격 시작 표시)
        float colorChangeDuration = 0.2f;
        float elapsed = 0f;
        while (elapsed < colorChangeDuration)
        {
            sr.color = Color.Lerp(originalColor, Color.magenta, elapsed / colorChangeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = Color.magenta;

        // 공격 시작
        if (col != null) col.enabled = true;
        if (childEffect != null) childEffect.SetActive(true);

        yield return new WaitForSeconds(hitDuration);

        // 공격 종료
        if (col != null) col.enabled = false;
        if (childEffect != null) childEffect.SetActive(false);
        obj.SetActive(false);
        sr.color = originalColor;
    }

    private IEnumerator BlinkEffect(SpriteRenderer sr, float duration)
    {
        float blinkInterval = 0.2f;
        float elapsed = 0f;

        Color originalColor = sr.color;
        float minAlpha = 0.3f;
        float maxAlpha = originalColor.a;

        while (elapsed < duration)
        {
            float t = Mathf.PingPong(Time.time * (1f / blinkInterval), 1f);
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, t);
            sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);

            elapsed += Time.deltaTime;
            yield return null;
        }

        sr.color = originalColor;
    }
}
