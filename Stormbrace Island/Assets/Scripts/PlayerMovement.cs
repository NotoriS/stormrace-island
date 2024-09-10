using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _acceleration;
    [SerializeField]
    private float _maxSpeed;

    private GameActions _gameActions;
    private Rigidbody _rigidBody;

    public void Awake()
    {
        _gameActions = new GameActions();
        _rigidBody = GetComponent<Rigidbody>();
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

    public void FixedUpdate()
    {
        Vector2 inputDirection = _gameActions.Player.PlayerMovement.ReadValue<Vector2>().normalized;
        Debug.Log(inputDirection);

        Vector3 newVelocity = _rigidBody.velocity;
        newVelocity.x += inputDirection.x * _acceleration * Time.fixedDeltaTime;
        newVelocity.z += inputDirection.y * _acceleration * Time.fixedDeltaTime;

        if (newVelocity.magnitude > _maxSpeed)
        {
            newVelocity = newVelocity.normalized * _maxSpeed;
        }
        
        _rigidBody.velocity = newVelocity;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _rigidBody.AddForce(new Vector3(0f, 300f, 0f));
    }
}
