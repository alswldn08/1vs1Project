using System.Collections;
using UnityEngine;

public enum ProjectileType { Straight, Homing, QuadraticHoming, CubicHoming }

public class SkillEmission : MonoBehaviour
{
    [SerializeField] private ProjectileType projectileType = ProjectileType.Straight;
    [SerializeField] private int bulletCount = 5;
    [SerializeField] private float cooldownTime = 2f;
    [SerializeField] private GameObject[] bullets;
    [SerializeField] private Transform skillSpawnPoint;
    [SerializeField] private Transform target;

    [SerializeField, Range(0f, 1f)]
    private float fireTiming = 0.8f;

    private int currentBulletIndex = 0;
    private float attackRate = 0.05f;

    private Coroutine attackRoutine;
    public GameObject potal;
    private BossHP bossHP;
    private Animator bossAnim;

    public bool isDead = false; // 죽음 상태 체크

    void Start()
    {
        bossHP = GetComponentInParent<BossHP>();
        bossAnim = GetComponentInParent<Animator>();
        bossAnim.SetInteger("BossState", 0); // 초기 Idle
    }

    public void Update()
    {
        if (isDead)
        {
            StopSkill();
        }
    }

    public void StartSkill()
    {
        if (attackRoutine == null && !isDead)
        {
            attackRoutine = StartCoroutine(SkillRoutine());
            bossHP.bossUI.SetActive(true);
        }
    }

    public void StopSkill()
    {
        if (isDead) return; // 이미 죽었으면 처리하지 않음

        // 1. 코루틴 종료
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);

            SoundManager.i.bgmSound.Stop();
            SoundManager.i.PlayEffect(8);
            attackRoutine = null;
        }

        bossAnim.SetTrigger("Death");          // 오직 Death 트리거만 켜기

        // 3. 포탈 활성화
        potal.SetActive(true);
    }



    private IEnumerator SkillRoutine()
    {
        while (!isDead)
        {
            // 1. 애니메이션 번호 결정
            int animNumber = (projectileType == ProjectileType.Homing || projectileType == ProjectileType.CubicHoming) ? 1 : 2;
            bossAnim.SetInteger("BossState", animNumber);

            // 2. 현재 애니메이션 길이 가져오기
            AnimatorClipInfo[] clipInfos = bossAnim.GetCurrentAnimatorClipInfo(0);
            float animLength = 1f;
            if (clipInfos.Length > 0)
            {
                animLength = clipInfos[0].clip.length;
            }

            // 3. 발사 타이밍까지 대기
            yield return new WaitForSeconds(animLength * fireTiming);

            // 4. projectileType 랜덤 선택
            projectileType = (ProjectileType)Random.Range(0, System.Enum.GetValues(typeof(ProjectileType)).Length);

            // 5. 총알 발사
            for (currentBulletIndex = 0; currentBulletIndex < bulletCount; currentBulletIndex++)
            {
                if (isDead) 
                {
                    break; 
                }

                GameObject clone = Instantiate(
                    bullets[(int)projectileType],
                    skillSpawnPoint.position,
                    Quaternion.identity
                );
                clone.GetComponent<BossPatternBase>().Setup(target, 1, bulletCount, currentBulletIndex);

                yield return new WaitForSeconds(attackRate);
            }

            // 6. 남은 애니메이션 시간 대기
            float remainTime = animLength * (1f - fireTiming);
            yield return new WaitForSeconds(remainTime);

            // 7. Idle 복귀 (죽음 상태면 실행하지 않음)
            if (!isDead)
                bossAnim.SetInteger("BossState", 0);

            // 8. 쿨타임 대기
            yield return new WaitForSeconds(cooldownTime);
        }
    }
}
