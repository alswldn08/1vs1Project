using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCubicHomingPattern : BossPatternBase
{
    private Vector2 start, end, point1, point2;
    private float duration, t = 0f;
    private Transform target;

    public override void Setup(Transform target, float damage, int maxCount = 1, int index = 0)
    {
        base.Setup(target, damage);

        this.target = target;
        start = transform.position;
        end = this.target.position;

        float distance = Vector3.Distance(start, end);
        duration = distance / bossMovement.MoveSpeed;

        float angle = -60;
        angle += Utils.GetAngleFromProsition(start, end);

        point1 = Utils.GetNewPoint(start, angle, distance * 0.3f);
        point2 = Utils.GetNewPoint(start, angle * -1, distance * 0.7f);
    }

    public override void Process()
    {
        end = target.position;
        t += Time.deltaTime / duration;
        transform.position = Utils.CubicCurve(start, point1, point2, end, t);
    }
}
