using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardVelocityController
{
    //地面を移動するときx方向の加速、減速時の加速度
    private const float GroundAcceleration = 20f;
    private const float GroundDeceleration = 20f;
    //空中にいるときのx方向の加速、減速時の加速度
    private const float AirborneAccelProportion = 1.0f;
    private const float AirborneDecelProportion = 0.2f;
    //ジャンプ中の重力
    private const float JumpGravity = 20f;
    //落下中の重力
    private const float FallGravity = 25f;
    //地上にいるときのy方向の速度
    private const float GroundStickVelocity = 0f;
    //ジャンプ中にボタンを離したときジャンプカットする値
    private const float JumpCutRatio = 0.5f;

    public Vector2 UpdateVelocity(WizardVelocityContext context)
    {
        float nextSpeed_x;
        float nextSpeed_y;

        //プレイヤーが地上にいるときの処理
        var isStanding = (context.CurrentState & WizardStateFlags.Standing) != 0;
        if (isStanding)
        {
            nextSpeed_x = GroundedHorizontalMovement(context.Model, context.XAxis, context.CurrentVelocity.x);
            nextSpeed_y = context.JumpRequested ? context.Model.JumpVelocity : GroundedVerticalMovement();
        }
        //プレイヤーが空中にいるときの処理
        else
        {
            nextSpeed_x = AirborneHorizontalMovement(context.Model, context.XAxis, context.CurrentVelocity.x);
            nextSpeed_y = AirborneVerticalMovement(context.CurrentVelocity.y, context.CurrentState, context.JumpRelesed);
        }

        return new Vector2(nextSpeed_x, nextSpeed_y);
    }

    //地面の上を移動するときのx方向の速度の計算を行う関数
    public float GroundedHorizontalMovement(WizardModel model, float xAxis, float currentSpeed_x)
    {
        float maxSpeed = model.CalculateRunSpeed();
        float desiredSpeed = xAxis != 0f ? maxSpeed : 0f;
        float acceleration = xAxis != 0f ? GroundAcceleration : GroundDeceleration;
        return Mathf.MoveTowards(currentSpeed_x, desiredSpeed, acceleration * Time.fixedDeltaTime);
    }

    private float GroundedVerticalMovement()
    {
        return GroundStickVelocity;
    }

    //空中で移動するときのx方向の速度の計算を行う関数
    private float AirborneHorizontalMovement(WizardModel model, float xAxis, float currentSpeed_x)
    {
        float maxSpeed = model.CalculateRunSpeed();
        float desiredSpeed = xAxis != 0f ? maxSpeed : 0f;
        float acceleration = xAxis != 0f ? GroundAcceleration * AirborneAccelProportion : GroundDeceleration * AirborneDecelProportion;
        return Mathf.MoveTowards(currentSpeed_x, desiredSpeed, acceleration * Time.fixedDeltaTime);
    }

    private float AirborneVerticalMovement(float currentSpeed_y, WizardStateFlags currentState, bool jumpRelesed)
    {
        var touchingCeiling = (currentState & WizardStateFlags.TouchingCeiling) != 0;
        var isJumping = (currentState & WizardStateFlags.Jumping) != 0;

        //ジャンプカット
        if(jumpRelesed && currentSpeed_y > 0f)
            currentSpeed_y *= JumpCutRatio;

        //落下中の減速
        if (!isJumping || (touchingCeiling && currentSpeed_y > 0f))
            currentSpeed_y -= FallGravity * Time.fixedDeltaTime;
        //ジャンプ中の減速
        else
            currentSpeed_y -= JumpGravity * Time.fixedDeltaTime;

        return currentSpeed_y;
    }
}
