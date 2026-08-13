using UnityEditor;
using UnityEngine;

public class SampleEnemyPresenter : EnemyPresenter
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed;

    private CheckGroundCollision _groundChecker;
    private CheckWallCollision _wallChecker;
    private CheckNextGroundCollision _nextGroundChecker;

    private bool _isGrounded = false;
    private bool _isWalled = false;
    private bool _isNextGrounded = false;
    private bool _stopRequest = false;

    private const float GroundRaycastDistance = 0.15f;
    private const float WallRaycastDistance = 0.4f;
    private const float GroundAdjustValueY = 0f; //地面用Raycastのy座標の生成位置を調整する値
    private const float NextGroundAdjustValueX = 0.8f; //移動先地面用Raycastのx座標の生成位置を調整する値
    private const float Acceleration = 12f;
    private const float GroundStickVelocity = 0f;
    private const float Gravity = -4f;

    public override void ManualStart()
    {
        base.ManualStart();

        var collider = GetComponent<BoxCollider2D>();
        _groundChecker = new CheckGroundCollision(GroundRaycastDistance, _layerMask, collider, GroundAdjustValueY);
        _wallChecker = new CheckWallCollision(WallRaycastDistance, _layerMask, collider);
        //CheckNextGroundCollisionクラスも地面を判定するクラスのためCheckWallCollisionクラスの定数を使っている
        _nextGroundChecker = new CheckNextGroundCollision(GroundRaycastDistance, _layerMask, collider, NextGroundAdjustValueX, GroundAdjustValueY);
        _groundChecker.ConfigureContactFilter2D();
        _wallChecker.ConfigureContactFilter2D();
        _nextGroundChecker.ConfigureContactFilter2D();

        _view.SetDirectionScale(_model.Direction);
    }

    public override void ManualFixedUpdate()
    {
        base.ManualFixedUpdate();

        if (_isActivated)
        {
            //当たり判定の確認
            _isGrounded = _groundChecker.CheckCollision(_model.Direction);

            _isWalled = _wallChecker.CheckCollision(_model.Direction);
            _isNextGrounded = _nextGroundChecker.CheckCollision(_model.Direction);

            //壁がある、移動先に地面が無いときに反対方向を向く
            if (!_stopRequest && (_isWalled || !_isNextGrounded))
            {
                _stopRequest = true;
            }

            //Boarの視界にプレイヤーがいるかで代入する速さを変える
            float velocityX = CalculateVelocityX();

            float velocityY = _isGrounded ? GroundStickVelocity : Gravity;

            _rb2d.linearVelocity = new Vector2(velocityX, velocityY);

        }
        else if (!_isActivated && _model.Status.HitPoint != 0)
        {
            _rb2d.linearVelocity = Vector2.zero;
        }
    }

    //X方向の速度を計算する
    private float CalculateVelocityX()
    {
        if (_stopRequest)
        {
            var velocityX = Mathf.MoveTowards(_rb2d.linearVelocityX, 0f, Acceleration * Time.fixedDeltaTime);

            if (Mathf.Approximately(velocityX, 0f))
            {
                _model.Direction *= -1;
                _view.SetDirectionScale(_model.Direction);
                _stopRequest = false;
            }

            return velocityX;
        }


        float maxSpeed = _speed * _model.Direction;

        return Mathf.MoveTowards(_rb2d.linearVelocityX, maxSpeed, Acceleration * Time.fixedDeltaTime);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        var collider = GetComponent<BoxCollider2D>();
        var bounds = collider.bounds;

        //Ground
        Handles.color = Color.red;
        var startPosition = new Vector2(bounds.center.x, bounds.min.y);
        var startPositions = new Vector2[3];
        startPositions[0] = startPosition + Vector2.right * bounds.size.x * 0.4f;
        startPositions[1] = startPosition;
        startPositions[2] = startPosition + Vector2.left * bounds.size.x * 0.4f;
        foreach (var position in startPositions)
        {
            Handles.DrawLine(position, position + Vector2.down * GroundRaycastDistance);
        }

        //Wall
        Handles.color = Color.blue;
        var positionX = direction == 1 ? bounds.max.x : bounds.min.x;
        var startPosition2 = new Vector2(positionX, bounds.center.y);
        var startPositions2 = new Vector2[3];
        startPositions2[0] = startPosition2 + Vector2.up * bounds.size.y * 0.4f;
        startPositions2[1] = startPosition2;
        startPositions2[2] = startPosition2 + Vector2.down * bounds.size.y * 0.4f;
        foreach (var position in startPositions2)
        {
            Handles.DrawLine(position, position + new Vector2(direction * WallRaycastDistance, 0));
        }

        //NextGround
        Handles.color = Color.green;
        var startPosition3 = startPosition + new Vector2(NextGroundAdjustValueX, 0);
        Handles.DrawLine(startPosition3, startPosition3 + Vector2.down * GroundRaycastDistance);

        Handles.color = Color.white;
    }
#endif
}
