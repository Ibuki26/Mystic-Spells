using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WizardPresenter : MonoBehaviour
{
    [SerializeField] private WizardModelData _modelData;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private ShotMagicConfigData _configData;

    private PlayerInput _playerinput;
    private WizardModel _model;
    private WizardView _view;
    private WizardStateFlags _stateFlags;
    private Rigidbody2D _rb2d;
    private WizardVelocityController _velocityController;
    private CheckGroundCollision _groundChecker;
    private CheckCeilingCollision _ceilingChecker;
    private ICastCommand _castCommand;
    private IMagicConfig _config;

    private float _xAxis = 0f; //x方向の入力状態を記録する
    private bool _jumpRequested = false;
    private bool _castRequested = false;

    [SerializeField] private float RaycastDistance = 0.09f;
    [SerializeField] private float GroundAdjustValueY = 0.11f; //Raycastのy座標の生成位置を調整する値
    private const float MoveThreshold = 0.2f; //x方向の入力の閾値

    //Debug用
    public WizardStateFlags StateFlags => _stateFlags;
    public WizardModel Model => _model;

    private void Awake()
    {
        _playerinput = GetComponent<PlayerInput>();
    }

    // PlayerInputへの関数登録
    private void OnEnable()
    {
        if (_playerinput == null) return;

        _playerinput.onActionTriggered += OnMove;
        _playerinput.onActionTriggered += OnJump;
        _playerinput.onActionTriggered += OnCast;
    }

    // PlayerInputへの関数登録解除
    private void OnDisable()
    {
        if (_playerinput == null) return;

        _playerinput.onActionTriggered -= OnMove;
        _playerinput.onActionTriggered -= OnJump;
        _playerinput.onActionTriggered -= OnCast;
    }

    public void ManualStart()
    {
        _model = new WizardModel(_modelData);
        _model.SetMoveSpeedStrategy(new NormalMoveSpeed());
        _model.SetDamageStrategy(new NormalDamage());
        _model.SetHealStrategy(new NormalHeal());

        _view = GetComponent<WizardView>();
        _view.Initialize();

        _stateFlags = default;
        _rb2d = GetComponent<Rigidbody2D>();
        _velocityController = new WizardVelocityController();

        var collider = GetComponent<CapsuleCollider2D>();
        _groundChecker = new CheckGroundCollision(RaycastDistance, _layerMask, collider, GroundAdjustValueY);
        _ceilingChecker = new CheckCeilingCollision(RaycastDistance, _layerMask, collider);
        _groundChecker.ConfigureContactFilter2D();
        _ceilingChecker.ConfigureContactFilter2D();

        // 魔法情報の初期設定
        _config = new ShotMagicConfig(_configData);
        _castCommand = new ShotMagicCommand(new ShotMagicFactory());
    }

    public void ManualUpdate()
    {
        UpdateJumpAnimation();
        UpdateMoveAnimation();
    }

    public void ManualFixedUpdate()
    {
        UpdateStandingFlag();
        HandleLanding();
        UpdateCeilingFlag();

        _rb2d.linearVelocity = _velocityController.UpdateVelocity(_rb2d.linearVelocity, _jumpRequested, _model, _xAxis, _stateFlags);
        StartJump();

        StartCast();
    }

    #region 移動関連
    // x方向の入力を取得する
    private void OnMove(InputAction.CallbackContext context)
    {
        if (context.action.name != "Move") return;

        float xAxis = context.ReadValue<float>();
        
        _xAxis = xAxis;
    }

    // x方向の入力情報からキャラクターの向きを更新する
    private void UpdateMoveAnimation()
    {
        bool isMoving = Mathf.Abs(_xAxis) > MoveThreshold;

        if (isMoving)
        {
            _model.Direction = (int)Mathf.Sign(_xAxis);
            _view.SetDirectionScale(_model.Direction);
        }
        
        _view.SetAnimation("isRun", isMoving);
    }
    #endregion

    #region ジャンプ関連
    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.action.name != "Jump") return;

        if (context.performed)
            TryJump();

        if (context.canceled)
            EndJump();
    }

    // ジャンプ入力を受け取り、ジャンプ要求フラグを立てる（この時点ではジャンプしない）
    private void TryJump()
    {
        var isStanding = (_stateFlags & WizardStateFlags.Standing) != 0;
        if (isStanding)
        {
            _jumpRequested = true;
        }
    }

    // ボタンを離し、ジャンプ状態を解除する
    private void EndJump()
    {
        var isJumping = (_stateFlags & WizardStateFlags.Jumping) != 0;
        if (isJumping)
        {
            _stateFlags &= ~WizardStateFlags.Jumping;
        }
    }

    // ジャンプを開始し、リクエストを終わらせる
    private void StartJump()
    {
        var isStanding = (_stateFlags & WizardStateFlags.Standing) != 0;
        if (isStanding && _jumpRequested)
        {
            _stateFlags |= WizardStateFlags.Jumping;
            _jumpRequested = false;
        }
    }

    private void UpdateJumpAnimation()
    {
        bool isJumping = (_stateFlags & WizardStateFlags.Jumping) != 0;
        _view.SetAnimation("isJump", isJumping);
    }
    #endregion

    private void OnCast(InputAction.CallbackContext context)
    {
        var isCasting = (_stateFlags & WizardStateFlags.Casting) != 0;
        var isReady = _castCommand.IsReady();
        if (context.action.name != "Cast" || isCasting || !isReady) return;

        _castRequested = true;
    }

    private void StartCast()
    {
        if (_castRequested)
        {
            _stateFlags |= WizardStateFlags.Casting;
            _castRequested = false;

            _view.SetAnimationTrigger("attack");

            var castContext = new CastContext(transform.position, _model.Direction, _model.Status.Strength);
            _castCommand.Execute(_config, castContext);
        }
    }

    //Animation Eventから呼び出される
    public void EndCast()
    {
        _stateFlags &= ~WizardStateFlags.Casting;
    }

    //地面と接触しているか確認する
    private void UpdateStandingFlag()
    {
        var isGrounded = _groundChecker.CheckCollision(_model.Direction);
        if (isGrounded)
            _stateFlags |= WizardStateFlags.Standing;
        else
            _stateFlags &= ~WizardStateFlags.Standing;
    }

    // 着地時にジャンプ状態を解除する
    //ボタンを押したままキャラクターが着地した時用
    private void HandleLanding()
    {
        var isStanding = (_stateFlags & WizardStateFlags.Standing) != 0;
        var isjumping = (_stateFlags & WizardStateFlags.Jumping) != 0;
        var isFalling = _rb2d.linearVelocity.y < 0.0f;
        
        if(isStanding && isjumping && isFalling)
        {
            _stateFlags &= ~WizardStateFlags.Jumping;
        }
    }

    //天井と接触しているか確認する
    private void UpdateCeilingFlag()
    {
        var isCeilinged = _ceilingChecker.CheckCollision(_model.Direction);
        if (isCeilinged)
            _stateFlags |= WizardStateFlags.TouchingCeiling;
        else
            _stateFlags &= ~WizardStateFlags.TouchingCeiling;
    }
}
