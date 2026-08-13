using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMagicCommand : ICastCommand
{
    private ShotMagicFactory _factory;
    private Timer _cooldownTimer;

    public ShotMagicCommand(ShotMagicFactory factory)
    {
        _factory = factory;
        _cooldownTimer = new Timer();
    }

    //à¯êîÇContextÇ…Ç∑ÇÈÇ©Ç‡
    public void Execute(IMagicConfig config, CastContext castContext)
    {
        //âπê∫
        //UIï\é¶
        var shotMagicConfig = (ShotMagicConfig)config;

        var spawnPosition = CalculateSpawnPosition(castContext.Position, shotMagicConfig.SpawnOffset, castContext.Direction);
        _factory.Create(shotMagicConfig, spawnPosition, castContext.Strength, castContext.Direction);

        _cooldownTimer.StartTimer(shotMagicConfig.CoolTime);
    }

    public bool UpdateCooldown(float deltaTime)
    {
        return _cooldownTimer.UpdateTimer(deltaTime);
    }

    public bool IsReady()
    {
        return !_cooldownTimer.IsRunning;
    }

    private Vector3 CalculateSpawnPosition(Vector3 position, Vector3 offset, int direction)
    {
        return position + new Vector3(
            offset.x * direction,
            offset.y,
            offset.z
        );
    }
}
