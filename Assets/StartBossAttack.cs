using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossAttack : MonoBehaviour
{
    private BossMovement bossMovement;
    // Start is called before the first frame update
    void Start()
    {
        bossMovement = FindObjectOfType<BossMovement>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            bossMovement.StartBossAttack();
        }
    }
}
