using UnityEngine;

public class StartBossAttack : MonoBehaviour
{
    private SkillEmission skillEmission;

    private void Start()
    {
        skillEmission = FindObjectOfType<SkillEmission>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skillEmission.StartSkill();
        }
    }
}
