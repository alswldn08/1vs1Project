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

    [SerializeField]
    [Range(0f, 1f)]
    private float fireTiming = 0.8f; // 애니메이션 진행 비율(끝나기 직전 발사)

    private int currentBulletIndex = 0;
    private float attackRate = 0.05f;

    private Coroutine attackRoutine;
    public GameObject potal;
    private BossHP bossHP;
    private Animator bossAnim;

    void Start()
    {
        bossHP = GetComponentInParent<BossHP>();
        bossAnim = GetComponentInParent<Animator>();
        bossAnim.SetInteger("BossState", 0); // 기본 idle 상태
    }

    public void StartSkill()
    {
        if (attackRoutine == null)
        {
            attackRoutine = StartCoroutine(SkillRoutine());
            bossHP.bossUI.SetActive(true);
        }
    }

    public void StopSkill()
    {
        if (attackRoutine != null)
        {
            StopCoroutine(attackRoutine);
            potal.gameObject.SetActive(true);
            attackRoutine = null;
            bossAnim.SetInteger("BossState", 0); // 강제 idle 복귀
        }
    }

    private IEnumerator SkillRoutine()
    {
        while (true)
        {
            // 1. 애니메이션 번호 결정
            int animNumber = (projectileType == ProjectileType.Homing || projectileType == ProjectileType.CubicHoming) ? 1 : 2;
            bossAnim.SetInteger("BossState", animNumber);

            // 2. 애니메이션 길이 가져오기
            AnimatorClipInfo[] clipInfos = bossAnim.GetCurrentAnimatorClipInfo(0);
            float animLength = 1f; // 기본값
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

            // 7. Idle로 복귀
            bossAnim.SetInteger("BossState", 0);

            // 8. 쿨타임 대기
            yield return new WaitForSeconds(cooldownTime);
        }
    }
}
