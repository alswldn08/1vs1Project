using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class BossPatternBase : MonoBehaviour
{
    [SerializeField]
    private GameObject hitEffect;
    protected BossMovement bossMovement;

    public virtual void Setup(Transform target, float damage, int maxCount = 1, int index = 0)
    {
        bossMovement = GetComponent<BossMovement>();
    }

    private void Update()
    {
        Process();
    }

    public abstract void Process();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
