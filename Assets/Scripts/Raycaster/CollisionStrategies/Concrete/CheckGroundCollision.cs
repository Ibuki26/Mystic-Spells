using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地面の当たり判定を確認するクラス
public class CheckGroundCollision : BaseCheckCollision
{
    private float _adjustedRaycastValueY; //Raycastの発射点のy座標を調整する数値

    private const float RaycastEdgeOffsetRatio = 0.4f;
    private const int DummyDirection = 1;

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

    //地面のオブジェクトから指定のコンポーネントを取得する
    public bool TryGetGroundComponent<T>(out T component) where T : Component
    {
        component = null;
        var bounds = _collider.bounds;
        Vector2 start = GetRaycastStart(bounds, DummyDirection);
        Vector2 raycastDirection = GetDirection(DummyDirection);

        _raycastPositions = GetRaycastPositions(bounds, start);

        for (int i = 0; i < _raycastPositions.Length; i++)
        {
            int count = Physics2D.Raycast(_raycastPositions[i], raycastDirection, _contactFilter, _hitBuffers, _raycastDistance);

            Debug.DrawRay(_raycastPositions[i],
              raycastDirection * _raycastDistance,
              Color.red);

            if (count != 0 && IsHitValid(bounds, _hitBuffers[0], DummyDirection))
            {
                var result = _hitBuffers[0].collider.TryGetComponent(out component);
                ClearBuffer();
                return result;
            }
        }

        ClearBuffer();
        return false;
    }
}
