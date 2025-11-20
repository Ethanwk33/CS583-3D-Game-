using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    public float MovementSpeed = 10f;
    public float RotationSpeed = 5f;
    public float JumpForce = 10f;
    public float Gravity = -30f;

    private float _rotationY;
    private float _verticalVelocity;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 movementVector)
    {
        // Horizontal movement
        Vector3 move = transform.forward * movementVector.y + transform.right * movementVector.x;
        move *= MovementSpeed;

        // Reset downward velocity when grounded
        if (_characterController.isGrounded && _verticalVelocity < 0)
            _verticalVelocity = -1f;

        // Apply gravity
        _verticalVelocity += Gravity * Time.deltaTime;

        // Add vertical movement
        move.y = _verticalVelocity;

        // Move the character
        _characterController.Move(move * Time.deltaTime);
    }

    public void Rotate(Vector2 rotationVector)
    {
        _rotationY += rotationVector.x * RotationSpeed * Time.deltaTime;
        transform.localRotation = Quaternion.Euler(0, _rotationY, 0);
    }

    public void Jump()
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = JumpForce;
        }
    }
}
