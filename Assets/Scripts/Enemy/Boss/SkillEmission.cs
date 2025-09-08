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

    private int currentBulletIndex = 0;
    private float attackRate = 0.05f;

    private Coroutine attackRoutine;
    private BossHP bossHP;
    public GameObject potal;

    void Start()
    {
        bossHP = FindObjectOfType<BossHP>();
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
        }
    }

    private IEnumerator SkillRoutine()
    {
        while (true)
        {
            projectileType = (ProjectileType)Random.Range(0, System.Enum.GetValues(typeof(ProjectileType)).Length);

            for (currentBulletIndex = 0; currentBulletIndex < bulletCount; currentBulletIndex++)
            {
                GameObject clone = Instantiate(bullets[(int)projectileType], skillSpawnPoint.position, Quaternion.identity);
                clone.GetComponent<BossPatternBase>().Setup(target, 1, bulletCount, currentBulletIndex);

                yield return new WaitForSeconds(attackRate);
            }

            yield return new WaitForSeconds(cooldownTime);
        }
    }
}
