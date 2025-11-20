using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public PlayerController CharacterController;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _lookAction;
    private InputAction _jumpAction;

    void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Move"];
        _lookAction = _playerInput.actions["Look"];
        _jumpAction = _playerInput.actions["Jump"];

        _jumpAction.performed += OnJumpPerformed;
    }

    void OnEnable()
    {
        _moveAction.Enable();
        _lookAction.Enable();
        _jumpAction.Enable();
    }

    void OnDisable()
    {
        _moveAction.Disable();
        _lookAction.Disable();
        _jumpAction.Disable();
    }

    void Update()
    {
        CharacterController.Move(_moveAction.ReadValue<Vector2>());
        CharacterController.Rotate(_lookAction.ReadValue<Vector2>());
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        CharacterController.Jump();
    }
}
