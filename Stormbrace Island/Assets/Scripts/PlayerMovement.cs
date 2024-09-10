using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float decceleration;
    [SerializeField]
    private float jumpVelocity;

    [SerializeField]
    private float playerGravity;
    [SerializeField]
    private float maxMoveSpeed;
    [SerializeField]
    private float maxFallSpeed;

    private Vector2 _horizontalVelocity;
    private float _verticalVelocity = -0.01f;

    private GameActions _gameActions;
    private CharacterController _characterController;

    public void Awake()
    {
        _gameActions = new GameActions();
        _characterController = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        _gameActions.Player.Enable();
        _gameActions.Player.Jump.performed += OnJump;
    }

    private void OnDisable()
    {
        _gameActions.Player.Jump.performed -= OnJump;
        _gameActions.Player.Disable();
    }

    public void Update()
    {
        Vector2 inputDirection = _gameActions.Player.PlayerMovement.ReadValue<Vector2>().normalized;

        _horizontalVelocity.x += inputDirection.x * acceleration * Time.deltaTime;
        _horizontalVelocity.y += inputDirection.y * acceleration * Time.deltaTime;
        _verticalVelocity -= playerGravity * Time.deltaTime;

        if (_horizontalVelocity.magnitude - (decceleration * Time.deltaTime) < 0)
        {
            _horizontalVelocity = Vector2.zero;
        }
        else
        {
            _horizontalVelocity = _horizontalVelocity.normalized * (_horizontalVelocity.magnitude - (decceleration * Time.deltaTime));
        }

        if (_horizontalVelocity.magnitude > maxMoveSpeed)
        {
            _horizontalVelocity = _horizontalVelocity.normalized * maxMoveSpeed;
        }

        if (_characterController.isGrounded && _verticalVelocity < 0) _verticalVelocity = -0.01f;
        if (_verticalVelocity < -maxFallSpeed) _verticalVelocity = -maxFallSpeed;

        Vector3 velocity = new Vector3(_horizontalVelocity.x, _verticalVelocity, _horizontalVelocity.y);
        _characterController.Move(velocity * Time.deltaTime);

        if (_horizontalVelocity.magnitude > 0f)
        {
            Vector3 lookDirection = new Vector3(_horizontalVelocity.x, 0f, _horizontalVelocity.y);
            transform.rotation = Quaternion.LookRotation(lookDirection);
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (_characterController.isGrounded)
        {
            _verticalVelocity = jumpVelocity;
        }
    }
}
