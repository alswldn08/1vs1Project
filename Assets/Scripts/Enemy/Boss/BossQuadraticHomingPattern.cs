using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuadraticHomingPattern : BossPatternBase
{
    private Vector2 start, end, point;
    private float duration, t = 0f;
    private Transform target;

    [Range(0, 360)] public float b1;
    [Range(0, 360)] public float b2;

    public override void Setup(Transform target, float damage, int maxCount = 1, int index = 0)
    {
        base.Setup(target, damage);

        this.target = target;
        start = transform.position;
        end = this.target.position;

        float distance = Vector3.Distance(start, end);

        duration = distance / bossMovement.MoveSpeed;

        float angle = b1 + b2 * (index % 2);

        angle += Utils.GetAngleFromProsition(start, end);

        point = Utils.GetNewPoint(start, angle, distance * 0.3f);
    }
    public override void Process()
    {
        end = target.position;
        t += Time.deltaTime / duration;
        transform.position = Utils.QuadraticCurve(start, point, end, t);
    }
}
