using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _maxSpeed = 100f;
    [SerializeField]
    private float _jumpForce = 5f;
    [field:SerializeField]
    public bool IsSliding { get; private set; } = false;
    private bool _canJump = true;
    private Vector3 MoveDirection;
    private Rigidbody _rigidbody;
    private PhysicMaterial PhysicMat;
    [SerializeField]
    private Animator _animator;

    private void Start() {
        _rigidbody = GetComponent<Rigidbody>();
        PhysicMat = GetComponent<CapsuleCollider>().material;
    }

    private void FixedUpdate() {
        if(MoveDirection.x != 0) {
            if (!IsSliding) {
                _rigidbody.velocity = new Vector3(MoveDirection.x * _maxSpeed * Time.deltaTime, _rigidbody.velocity.y, _rigidbody.velocity.z);
            }  
        }

        if(_rigidbody.velocity.magnitude > 0) {
            _animator.SetBool("IsRunning", true);
        }
        else {
            _animator.SetBool("IsRunning", false);
        }
    }

    public void Move(InputAction.CallbackContext ctx) {
        MoveDirection = new Vector3(ctx.ReadValue<Vector2>().x, 0, 0);
    }

    public void Jump(InputAction.CallbackContext ctx) {
        if (!ctx.started || !_canJump) return;
        _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        _canJump = false;
        _animator.SetTrigger("IsJumping");
    }

    public void Slide(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            IsSliding = true;
            PhysicMat.dynamicFriction = 1f;
            PhysicMat.staticFriction = 1f;
            PhysicMat.frictionCombine = PhysicMaterialCombine.Minimum;
            _animator.SetBool("IsSliding", true);
        }
        if (ctx.canceled) {
            IsSliding = false;
            PhysicMat.dynamicFriction = 2f;
            PhysicMat.staticFriction = 2f;
            PhysicMat.frictionCombine = PhysicMaterialCombine.Average;
            _animator.SetBool("IsSliding", false);
        }
    }

    //Allow the player to jump again when he touches the ground
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            _canJump = true;
        }
    }
}
