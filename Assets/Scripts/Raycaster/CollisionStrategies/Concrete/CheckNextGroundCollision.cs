using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//移動先に地面があるか確認するクラス
public class CheckNextGroundCollision : BaseCheckCollision
{
    private float _adjustedRaycastValueX; //Raycastの発射点のx座標を調整する数値
    private float _adjustedRaycastValueY; //Raycastの発射点のy座標を調整する数値

    public CheckNextGroundCollision(float distance, LayerMask layerMask, Collider2D collider, float adjustX, float adjustY) : base(distance, layerMask, collider)
    {
        _adjustedRaycastValueX = adjustX;
        _adjustedRaycastValueY = adjustY;
    }

    protected override Vector2 GetRaycastStart(Bounds bounds, int direction)
    {
        return new Vector2(bounds.center.x, bounds.min.y) + new Vector2(_adjustedRaycastValueX * direction, 0);
    }

    protected override Vector2 GetDirection(int direction) => Vector2.down;

    protected override Vector2[] GetRaycastPositions(Bounds bounds, Vector2 start)
    {
        return new Vector2[] { start };
    }

    protected override bool IsHitValid(Bounds bounds, RaycastHit2D hit, int direction)
    {
        return bounds.min.y >= hit.point.y;
    }
}
