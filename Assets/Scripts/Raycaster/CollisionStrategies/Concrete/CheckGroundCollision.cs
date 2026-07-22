using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地面の当たり判定を確認するクラス
public class CheckGroundCollision : BaseCheckCollision
{
    private float _adjustedRaycastValueY; //Raycastの発射点のy座標を調整する数値

    private const float RaycastEdgeOffsetRatio = 0.4f;

    public CheckGroundCollision(float distance, LayerMask layerMask, Collider2D collider, float adjustY):base(distance, layerMask, collider)
    {
        _adjustedRaycastValueY = adjustY;
    }

    protected override Vector2 GetRaycastStart(Bounds bounds, int direction)
    {
        return new Vector2(bounds.center.x, bounds.min.y);
    }

    protected override Vector2 GetDirection(int direction) => Vector2.down;

    protected override Vector2[] GetRaycastPositions(Bounds bounds, Vector2 start)
    {
        var left = start + Vector2.left * bounds.size.x * RaycastEdgeOffsetRatio;
        var center = start;
        var right = start + Vector2.right * bounds.size.x * RaycastEdgeOffsetRatio;

        return new Vector2[] { left, center, right };
    }

    protected override bool IsHitValid(Bounds bounds, RaycastHit2D hit, int direction)
    {
        return bounds.min.y >= hit.point.y;
    }
}
