using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public GameObject[] attackObjects; // 공격 위치 오브젝트들 (미리 비활성화)
    [SerializeField] private float warningTime = 1f;   // 깜빡이는 시간
    [SerializeField] private float hitDuration = 1f;   // 공격 판정 시간
    [SerializeField] private float delayBetweenAttacks = 1f; // 다음 공격까지 대기 시간
    [SerializeField] private int simultaneousCount = 3;

    public bool isLooping = true;

    private void Start()
    {

    }


    public void StartBossAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (isLooping)
        {
            // 1. 랜덤으로 simultaneousCount 개 선택
            List<GameObject> selected = new List<GameObject>();
            while (selected.Count < simultaneousCount)
            {
                GameObject candidate = attackObjects[Random.Range(0, attackObjects.Length)];
                if (!selected.Contains(candidate))
                    selected.Add(candidate);
            }

            // 2. 각각을 병렬로 실행
            List<Coroutine> runningCoroutines = new List<Coroutine>();
            foreach (var obj in selected)
            {
                runningCoroutines.Add(StartCoroutine(AttackSequence(obj)));
            }

            // 3. 모두 끝날 때까지 대기
            foreach (var co in runningCoroutines)
            {
                yield return co;
            }

            // 4. 다음 공격까지 대기
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

        // 깜빡임
        yield return StartCoroutine(BlinkEffect(sr, warningTime));

        // 흰색으로 전환
        float colorChangeDuration = 0.2f;
        float elapsed = 0f;
        while (elapsed < colorChangeDuration)
        {
            sr.color = Color.Lerp(originalColor, Color.magenta, elapsed / colorChangeDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        sr.color = Color.magenta;

        // 공격 판정 시작
        if (col != null) col.enabled = true;
        if (childEffect != null) childEffect.SetActive(true);

        yield return new WaitForSeconds(hitDuration);

        // 공격 판정 종료
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

        // 깜빡임 끝났을 때 원래 색상(불투명)으로 복원
        sr.color = originalColor;
    }


}
