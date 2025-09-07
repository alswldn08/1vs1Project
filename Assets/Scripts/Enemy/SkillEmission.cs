using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ProjectileType { Straight, Homing, QuadraticHoming, CubicHoming }

public class SkillEmission : MonoBehaviour
{
    [SerializeField]
    private ProjectileType projectileType;
    [SerializeField]
    private float cooldownTime = 2f;
    [SerializeField]
    private GameObject[] projectiles;
    [SerializeField]
    private Transform skillSpawnPoint;
    [SerializeField]
    private Transform target;

    private float currentCooldownTime = 0;

    public bool IsSkillAvailable => (Time.time - currentCooldownTime > cooldownTime);

    void Start()
    {
        
    }

    void Update()
    {
        OnSkill();
    }

    public void OnSkill()
    {
        if (IsSkillAvailable == false) return;

        GameObject clone = GameObject.Instantiate(projectiles[(int)projectileType], skillSpawnPoint.position, Quaternion.identity);
        clone.GetComponent<BossPatternBase>().Setup(target, 1);
        currentCooldownTime = Time.time;
    }b
}
