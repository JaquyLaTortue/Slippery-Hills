using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed = 100f;
    [SerializeField]
    private float _jumpForce = 5f;
    [field:SerializeField]
    public bool IsCrouching { get; private set; } = false;
    public Vector3 MoveDirection;

    private Rigidbody _rigidbody;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        _rigidbody.velocity = new Vector3(MoveDirection.x * Time.deltaTime, _rigidbody.velocity.y, _rigidbody.velocity.z);
    }

    public void Move(InputAction.CallbackContext ctx) {
        if (IsCrouching) {
            MoveDirection = new Vector3(ctx.ReadValue<Vector2>().x * _maxSpeed / 2, 0, 0);
        }
        else {
            MoveDirection = new Vector3(ctx.ReadValue<Vector2>().x * _maxSpeed, 0, 0);
        }

    }

    public void Jump(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }
    }

    public void Crouch(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            IsCrouching = true;
        }
        if(ctx.canceled) {
            IsCrouching = false;
        }
    }
}
