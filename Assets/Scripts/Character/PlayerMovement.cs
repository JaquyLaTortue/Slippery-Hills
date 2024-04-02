using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 100f;
    [SerializeField]
    private bool _isCrouching = false;
    private Rigidbody _rigidbody;
    private Vector3 _moveDirection;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        _rigidbody.velocity = new Vector3(_moveDirection.x * Time.deltaTime, _rigidbody.velocity.y, _rigidbody.velocity.z);
    }

    public void Move(InputAction.CallbackContext ctx) {
        if (_isCrouching) {
            _moveDirection = new Vector3(ctx.ReadValue<Vector2>().x * _speed / 2, 0, 0);
        }
        else {
            _moveDirection = new Vector3(ctx.ReadValue<Vector2>().x * _speed, 0, 0);
        }

    }

    public void Jump(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            _rigidbody.AddForce(Vector3.up * 5, ForceMode.Impulse);
        }
    }

    public void Crouch(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            _isCrouching = true;
        }
        if(ctx.canceled) {
            _isCrouching = false;
        }
    }
}
