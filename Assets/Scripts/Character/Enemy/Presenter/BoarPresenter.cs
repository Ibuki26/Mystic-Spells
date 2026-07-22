using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoarPresenter : EnemyPresenter
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _speed;

    private CheckGroundCollision _groundChecker;
    private CheckWallCollision _wallChecker;
    private CheckNextGroundCollision _nextGroundChecker;
    private EnemySightChecker _sightChecker;
    private Timer _sightStateTimer;

    private bool _isGrounded = false;
    private bool _isWalled = false;
    private bool _isNextGrounded = false;
    private bool _isWizardInSight = false; //プレイヤーの発見状態を記録する
    private bool _isTimerUsing = false;

    private const float GroundRaycastDistance = 0.15f;
    private const float WallRaycastDistance = 0.4f;
    private const float GroundAdjustValueY = 0f; //地面用Raycastのy座標の生成位置を調整する値
    private const float NextGroundAdjustValueX = 0.8f; //移動先地面用Raycastのx座標の生成位置を調整する値
    private const float LostSightTime = 2f;
    private const float Acceleration = 12f;
    private const float AdditionalSpeed = 2f;
    private const float GroundStickVelocity = 0f;
    private const float Gravity = -4f;

    public override void ManualStart()
    {
        base.ManualStart();
        _sightChecker = GetComponent<EnemySightChecker>();
        _sightStateTimer = new Timer();

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
        if (_isActivated)
        {
            //地面と接触しているか確認
            _isGrounded = _groundChecker.CheckCollision(_model.Direction);

            _isWalled = _wallChecker.CheckCollision(_model.Direction);
            _isNextGrounded = _nextGroundChecker.CheckCollision(_model.Direction);

            //壁がある、移動先に地面が無いときに反対方向を向く
            if(_isWalled || !_isNextGrounded)
            {
                _model.Direction *= -1;
                _view.SetDirectionScale(_model.Direction);
            }

            UpdateSightState();

            //Boarの視界にプレイヤーがいるかで代入する速さを変える
            float velocityX = CalculateVelocityX();

            float velocityY = _isGrounded ? GroundStickVelocity : Gravity;

            _rb2d.linearVelocity = new Vector2(velocityX, velocityY);

        }
        else if(!_isActivated && _model.Status.HitPoint != 0)
        {
            _rb2d.linearVelocity = Vector2.zero;
        }
    }

    //X方向の速度を計算する
    private float CalculateVelocityX()
    {
        float maxSpeed = _isWizardInSight? _speed + AdditionalSpeed : _speed;

        return Mathf.MoveTowards(_rb2d.linearVelocityX, maxSpeed * _model.Direction, Acceleration * Time.fixedDeltaTime);
    }

    //プレイヤーの発見状態を管理する
    private void UpdateSightState()
    {
        var isSeeingWizard = _sightChecker.ScanForPlayer(_model.Direction);

        //プレイヤーが視界にいるときの処理
        if (isSeeingWizard)
        {
            _isWizardInSight = true;
            _isTimerUsing = false;
            return;
        }

        //プレイヤーが視界にいないときの処理
        //未発見状態でプレイヤーが視界にいなかったら処理を終了
        if (!_isWizardInSight)
            return;

        if (!_isTimerUsing)
        {
            _sightStateTimer.Start(LostSightTime);
            _isTimerUsing = true;
        }

         if (_sightStateTimer.IsReady())
        {
             _isWizardInSight = false;
            _isTimerUsing = true;
        }
    }
}
