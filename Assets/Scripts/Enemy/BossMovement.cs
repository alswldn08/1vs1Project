using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private BossHP bossHP;
    public GameObject[] attackObjects;
    public Vector2 target;         // 발사 직전 플레이어 위치
    public float speed = 5f;       // 이동 속도
    public int curveCount = 2;     // 몇 번 꺾을지 (1~2 추천)
    public int bulletNum = 10;
    private Vector2 startPos;
    private Vector2[] controlPoints;
    private float bulletProgress = 0f;
    public GameObject potal;
    public bool isLooping = true;

    private void Start()
    {
        bossHP = FindObjectOfType<BossHP>();

        startPos = transform.position;

        controlPoints = new Vector2[curveCount + 2];


        for (int i = 1; i <= curveCount; i++)
        {
            Vector2 mid = Vector2.Lerp(startPos, target, (float)i / (curveCount + 1));
            Vector2 offset = new Vector2(
                Random.Range(-2f, 2f),
                Random.Range(-2f, 2f));
            controlPoints[i] = mid + offset;
        }
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
        controlPoints[0] = startPos;
        controlPoints[controlPoints.Length - 1] = target;

        yield return new WaitForSeconds(8f);
    }
}
