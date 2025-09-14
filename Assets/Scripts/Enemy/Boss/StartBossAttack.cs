using UnityEngine;

public class StartBossAttack : MonoBehaviour
{
    private SkillEmission skillEmission;

    private void Start()
    {
        skillEmission = transform.parent.GetComponentInChildren<SkillEmission>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            skillEmission.StartSkill();
            SoundManager.i.PlayBGM(5);
        }
    }
}
