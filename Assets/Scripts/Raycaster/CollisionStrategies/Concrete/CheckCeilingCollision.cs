using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//天井の当たり判定を確認するクラス
public class CheckCeilingCollision : BaseCheckCollision
{
    private const float RaycastEdgeOffsetRatio = 0.4f;

    public CheckCeilingCollision(float distance, LayerMask layerMask, Collider2D collider) : base(distance, layerMask, collider) { }

    protected override Vector2 GetRaycastStart(Bounds bounds, int direction)
    {
        return new Vector2(bounds.center.x, bounds.max.y);
    }

    protected override Vector2 GetDirection(int direction) => Vector2.up;

    protected override Vector2[] GetRaycastPositions(Bounds bounds, Vector2 start)
    {
        var left = start + Vector2.left * bounds.size.x * RaycastEdgeOffsetRatio;
        var center = start;
        var right = start + Vector2.right * bounds.size.x * RaycastEdgeOffsetRatio;

        return new Vector2[] { left, center, right };
    }

    protected override bool IsHitValid(Bounds bounds, RaycastHit2D hit, int direction)
    {
        return bounds.max.y <= hit.point.y;
    }
}
