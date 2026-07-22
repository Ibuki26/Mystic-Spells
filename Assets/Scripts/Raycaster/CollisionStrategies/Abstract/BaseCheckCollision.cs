using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//地面や壁などの当たり判定を確認するクラスの抽象クラス
public abstract class BaseCheckCollision
{
    protected float _raycastDistance; //Raycastの線の長さ
    protected LayerMask _layerMask;
    private readonly Collider2D _collider;

    protected ContactFilter2D _contactFilter;
    protected Vector2[] _raycastPositions = new Vector2[3]; //Raycastの始点。左、中央、右で3つ
    protected RaycastHit2D[] _hitBuffers = new RaycastHit2D[5]; //Raycastの情報を記録する配列

    protected BaseCheckCollision(float distance, LayerMask layerMask,Collider2D collider)
    {
        _raycastDistance = distance;
        _layerMask = layerMask;
        _collider = collider;
    }

    public void ConfigureContactFilter2D()
    {
        _contactFilter.layerMask = _layerMask;
        _contactFilter.useLayerMask = true;
        _contactFilter.useTriggers = false;
    }

    //当たり判定の確認を行う関数
    //始点の計算、Raycastの方向、当たり判定の条件を関数で設定するようにし、その関数をオーバーライドすることで拡張しやすくしている
    public bool CheckCollision(int direction)
    {
        var bounds = _collider.bounds;
        Vector2 start = GetRaycastStart(bounds, direction);
        Vector2 raycastDirection = GetDirection(direction);

        _raycastPositions = GetRaycastPositions(bounds, start);

        for (int i = 0; i < _raycastPositions.Length; i++)
        {
            int count = Physics2D.Raycast(_raycastPositions[i], raycastDirection, _contactFilter, _hitBuffers, _raycastDistance);

            Debug.DrawRay(_raycastPositions[i],
              raycastDirection * _raycastDistance,
              Color.red);

            if (count != 0 && IsHitValid(bounds, _hitBuffers[0], direction))
            {
                ClearBuffer();
                return true;
            }
        }

        ClearBuffer();
        return false;
    }

    //始点の設定を行う関数
    protected abstract Vector2 GetRaycastStart(Bounds bounds, int direction);
    //方向の設定を行う関数
    protected abstract Vector2 GetDirection(int direction);
    //Raycastの始点の設定を行う関数
    protected abstract Vector2[] GetRaycastPositions(Bounds bounds, Vector2 start);
    //当たり判定の条件
    protected abstract bool IsHitValid(Bounds bounds, RaycastHit2D hit, int direction);

    //hitBuffersの初期化
    protected void ClearBuffer()
    {
        for (int j = 0; j < _hitBuffers.Length; j++)
        {
            _hitBuffers[j] = new RaycastHit2D();
        }
    }
}
