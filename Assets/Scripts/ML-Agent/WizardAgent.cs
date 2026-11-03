using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Input;
using UnityEngine.InputSystem;
using Unity.MLAgents.Sensors;

public class WizardAgent : Agent, IInputActionAssetProvider
{
    [SerializeField] private AreaObjectManager areaObjectManager;

    private Vector3 _startPosition;
    private PlayerInput _playerInput;
    private WizardPresenter _wizard;

    protected override void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public override void Initialize()
    {
        _startPosition = transform.position;
        _wizard = GetComponent<WizardPresenter>();
    }

    public override void OnEpisodeBegin()
    {
        transform.position = _startPosition;
        _wizard.ResetWizard();
        areaObjectManager.ResetGroup();
        UIManager.Instance.ResetScore();
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(_wizard.Model.Status.HitPoint);
        sensor.AddObservation(_wizard.Rigidbody2D.linearVelocity);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {

    }

    public (InputActionAsset, IInputActionCollection2) GetInputActionAsset()
    {
        _playerInput = GetComponent<PlayerInput>();
        return (_playerInput.actions, _playerInput.actions);
    }
}
