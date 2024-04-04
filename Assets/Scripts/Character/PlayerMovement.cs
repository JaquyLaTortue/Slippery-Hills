using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class handling the player's movement
/// </summary>
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
        // Move the player
        if(MoveDirection.x != 0) {
            if (!IsSliding) {
                _rigidbody.velocity = new Vector3(MoveDirection.x * _maxSpeed * Time.deltaTime, _rigidbody.velocity.y, _rigidbody.velocity.z);
            }  
        }

        // Rotate the player according to the ground
        Ray ray = new Ray(transform.position, -transform.up * 2);
        RaycastHit hit;
        Quaternion newRot = transform.rotation;
        if (Physics.Raycast(ray, out hit, 1.5f)) {
            if (hit.collider.CompareTag("Ground")) {
                newRot = hit.transform.rotation;
                Debug.Log(newRot.eulerAngles);
                if (transform.rotation.eulerAngles.y == 90) {
                    transform.rotation = Quaternion.Euler(-newRot.eulerAngles.z, transform.eulerAngles.y, transform.eulerAngles.z);
                }
                else {
                    transform.rotation = Quaternion.Euler(newRot.eulerAngles.z, transform.eulerAngles.y, transform.eulerAngles.z);
                }
            }
        }

        // Rotate the player according to the direction
        if (MoveDirection.x < 0) {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, -90, transform.eulerAngles.z);
        }
        else if (MoveDirection.x > 0) {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
        }

        // Controls the animations
        if (_rigidbody.velocity.magnitude > 0) {
            if (IsSliding) {
                _animator.SetBool("IsRunning", false);
            }
            else {
                _animator.SetBool("IsRunning", true);
            }
        }
        else {
            _animator.SetBool("IsRunning", false);
            _animator.SetBool("IsSliding", false);
        }
    }

    public void Move(InputAction.CallbackContext ctx) {
        MoveDirection = new Vector3(ctx.ReadValue<Vector2>().x, 0, 0);
    }

    public void Jump(InputAction.CallbackContext ctx) {
        if (!ctx.started || !_canJump) return;
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        _canJump = false;
        _animator.SetTrigger("IsJumping");
    }

    public void Slide(InputAction.CallbackContext ctx) {
        if (ctx.started) {
            IsSliding = true;
            PhysicMat.dynamicFriction = 0.5f;
            PhysicMat.staticFriction = 0.5f;
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

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Ground")) {
            _canJump = true;
        }
    }
}
