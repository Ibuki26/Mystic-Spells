using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//進行方向に壁があるか確認するクラス
public class CheckWallCollision : BaseCheckCollision
{
    private const float RaycastEdgeOffsetRatio = 0.4f;

    public CheckWallCollision(float distance, LayerMask layerMask, Collider2D collider) : base(distance, layerMask, collider) { }

    protected override Vector2 GetRaycastStart(Bounds bounds, int direction)
    {
        return new Vector2(bounds.center.x, bounds.min.y) + new Vector2(bounds.size.x * 0.5f * direction, bounds.size.y * 0.5f);
    }

    protected override Vector2 GetDirection(int direction)
    {
        return new Vector2(direction , 0);
    }

    protected override Vector2[] GetRaycastPositions(Bounds bounds, Vector2 start)
    {
        var top = start + Vector2.up * bounds.size.y * RaycastEdgeOffsetRatio;
        var middle = start;
        var bottom = start + Vector2.down * bounds.size.y * RaycastEdgeOffsetRatio;

        return new Vector2[] { top, middle, bottom };
    }

    protected override bool IsHitValid(Bounds bounds, RaycastHit2D hit, int direction)
    {
        if (direction > 0)
        {
            // 右向き → 壁が右にあるか
            return bounds.max.x <= hit.point.x;
        }
        else
        {
            // 左向き → 壁が左にあるか
            return bounds.min.x >= hit.point.x;
        }
    }
}
