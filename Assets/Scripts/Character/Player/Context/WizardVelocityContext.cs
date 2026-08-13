using UnityEngine;

public readonly struct WizardVelocityContext
{
    public Vector2 CurrentVelocity { get; }
    public bool JumpRequested { get; }
    public bool JumpRelesed { get; }
    public WizardModel Model { get; }
    public float XAxis { get; }
    public WizardStateFlags CurrentState { get; }

    public WizardVelocityContext(Vector2 velocity, bool jumpRequested, bool jumpRelesed, WizardModel model, float xAxis, WizardStateFlags stateFlags)
    {
        CurrentVelocity = velocity;
        JumpRequested = jumpRequested;
        JumpRelesed = jumpRelesed;
        Model = model;
        XAxis = xAxis;
        CurrentState = stateFlags;
    }
}
