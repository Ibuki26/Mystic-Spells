using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Input;
using UnityEngine.InputSystem;
using Unity.MLAgents.Sensors;

public class WizardAgent : Agent, IInputActionAssetProvider
{
    private Vector3 _startPosition;
    private PlayerInput _playerInput;
    private WizardPresenter _wizard;
    private EnvironmentManager _environmentManager;

    protected override void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public override void Initialize()
    {
        _startPosition = transform.position;
        _wizard = GetComponent<WizardPresenter>();
        _environmentManager = new EnvironmentManager();
    }

    public override void OnEpisodeBegin()
    {
        transform.position = _startPosition;
        _wizard.ResetWizard();
        AreaObjectManager.Instance.ResetGroup();
        UIManager.Instance.ResetUI();
        //ÉåÉbÉXÉìêîílÇÃéÊìæ
        
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_wizard.Model.Status.HitPoint);
        sensor.AddObservation(_wizard.Rigidbody2D.linearVelocity);
        sensor.AddObservation((_wizard.StateFlags & WizardStateFlags.Standing) != 0);
        sensor.AddObservation((_wizard.StateFlags & WizardStateFlags.Jumping) != 0);
        sensor.AddObservation((_wizard.StateFlags & WizardStateFlags.Casting) != 0);
        sensor.AddObservation((_wizard.StateFlags & WizardStateFlags.Invincible) != 0);
        sensor.AddObservation((_wizard.StateFlags & WizardStateFlags.TouchingCeiling) != 0);
    }
    
    public override void OnActionReceived(ActionBuffers actions)
    {
        AddReward(-1 / MaxStep); 
    }

    public (InputActionAsset, IInputActionCollection2) GetInputActionAsset()
    {
        _playerInput = GetComponent<PlayerInput>();
        return (_playerInput.actions, _playerInput.actions);
    }
}
