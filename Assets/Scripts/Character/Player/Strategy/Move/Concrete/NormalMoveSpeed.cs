using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//’Êí‚Ì‘¬“x‚ÌŒvZ
public class NormalMoveSpeed : IMoveSpeedStrategy
{
    private const float StandardSpeed = 3.0f;
    private const float SpeedConfficient = 0.025f;

    public float CalculateMoveSpeed(float speed)
    {
        return SpeedConfficient * speed + StandardSpeed;
    }
}
