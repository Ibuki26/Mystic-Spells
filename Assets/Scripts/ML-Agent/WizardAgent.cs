using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Input;
using UnityEngine.InputSystem;

public class WizardAgent : Agent, IInputActionAssetProvider
{
    private Vector3 _startPosition;
    private PlayerInput _playerInput;

    protected override void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    public override void Initialize()
    {
        _startPosition = transform.position;
    }

    public override void OnEpisodeBegin()
    {
        transform.position = _startPosition;
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
