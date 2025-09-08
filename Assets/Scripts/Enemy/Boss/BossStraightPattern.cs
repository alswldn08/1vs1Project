using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStraightPattern : BossPatternBase
{
    public override void Setup(Transform target, float damage, int maxCount = 1, int index = 0)
    {
        base.Setup(target, damage);

        bossMovement.MoveTo((target.position - transform.position).normalized);
    }

    public override void Process() { }
}
