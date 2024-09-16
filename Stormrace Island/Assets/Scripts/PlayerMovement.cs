using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Speed")]
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
    private float _verticalVelocity = 0;

    private GameActions _gameActions;
    private CharacterController _characterController;
    private Animator _characterAnimator;

    private Transform _cameraTransform;

    public void Awake()
    {
        _gameActions = new GameActions();
        _characterController = GetComponent<CharacterController>();
        _cameraTransform = GameObject.Find("VirtualPlayerCamera").transform;
        _characterAnimator = GetComponentInChildren<Animator>();
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
        _characterAnimator.SetFloat("Speed", 0f);
    }

    public void Update()
    {
        Vector2 inputDirection = _gameActions.Player.PlayerMovement.ReadValue<Vector2>().normalized;

        Vector2 camRight2D = new Vector2(_cameraTransform.right.x, _cameraTransform.right.z).normalized;
        Vector2 camForward2D = new Vector2(_cameraTransform.forward.x, _cameraTransform.forward.z).normalized;
        Vector2 relativeInputDirection = camRight2D * inputDirection.x + camForward2D * inputDirection.y;

        _horizontalVelocity.x += relativeInputDirection.x * acceleration * Time.deltaTime;
        _horizontalVelocity.y += relativeInputDirection.y * acceleration * Time.deltaTime;
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

        if (_verticalVelocity < -3f && _characterController.isGrounded) _verticalVelocity = -3f;
        if (_verticalVelocity < -maxFallSpeed) _verticalVelocity = -maxFallSpeed;

        Vector3 velocity = new Vector3(_horizontalVelocity.x, _verticalVelocity, _horizontalVelocity.y);
        _characterController.Move(velocity * Time.deltaTime);

        _characterAnimator.SetFloat("Speed", _horizontalVelocity.magnitude / maxMoveSpeed);
        _characterAnimator.SetBool("IsGrounded", _characterController.isGrounded);

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
            _characterAnimator.SetTrigger("Jump");
        }
    }
}
