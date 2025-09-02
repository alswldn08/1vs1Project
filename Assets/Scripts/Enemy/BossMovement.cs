using System.Collections;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    private BossHP bossHP;
    public GameObject potal;

    public GameObject bulletPrefab; // BossMissile 스크립트가 붙은 프리팹
    public Transform firePoint;
    public Transform player;

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
        int missileCount = 8;
        float spreadAngle = 60f;
        float fireInterval = 2f; // 공격 간격

        while (true) // 보스가 살아있는 동안 계속
        {
            for (int i = 0; i < missileCount; i++)
            {
                float angleOffset = Mathf.Lerp(-spreadAngle / 2f, spreadAngle / 2f, (float)i / (missileCount - 1));
                Quaternion rotation = firePoint.rotation * Quaternion.Euler(0, 0, angleOffset);

                GameObject missileObj = Instantiate(bulletPrefab, firePoint.position, rotation);
                BossMissile missile = missileObj.GetComponent<BossMissile>();
                if (missile != null)
                {
                    missile.SetTarget(player);
                }
            }

            yield return new WaitForSeconds(fireInterval); // 다음 공격까지 대기
        }
    }



}
