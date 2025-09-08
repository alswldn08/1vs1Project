using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHomingPattern : BossPatternBase
{
    private Transform target;

    public override void Setup(Transform target, float damage, int maxCount = 1, int index = 0)
    {
        base.Setup(target, damage);

        this.target = target;
    }

    public override void Process()
    {
        bossMovement.MoveTo((target.position - transform.position).normalized);
    }
}
