
using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Class handling the player's movement
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement Settings")]
    [SerializeField]
    private float _maxSpeed = 100f;

    [SerializeField]
    private float _jumpForce = 5f;

    private Sequence _squashSequence;

    public bool _canJump { get; private set; } = true;

    private Vector3 MoveDirection;
    private PhysicMaterial PhysicMat;

    public Rigidbody _rigidbody { get; private set; }

    private bool _isSliding = false;

    public bool IsSliding
    {
        get
        {
            return _isSliding;
        }
        private set
        {
            _isSliding = value;
            IsSlidingEvent?.Invoke(value);
        }
    }

    [field: SerializeField]
    public PlayerMain PlayerMain { get; private set; }

    public event Action UpdateJumpAnimation;
    public event Action<bool> UpdateRunningAnimation;
    public event Action<bool> UpdateSlideAnimation;
    public event Action<bool> IsSlidingEvent;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        PhysicMat = GetComponent<CapsuleCollider>().material;
    }

    private void FixedUpdate()
    {
        // Move the player
        if (MoveDirection.x != 0)
        {
            if (!IsSliding)
            {
                _rigidbody.velocity = new Vector3(MoveDirection.x * _maxSpeed * Time.deltaTime, _rigidbody.velocity.y, _rigidbody.velocity.z);
            }
        }

        // Rotate the player according to the ground
        Ray ray = new Ray(transform.position, -transform.up * 2);
        RaycastHit hit;
        Quaternion newRot = transform.rotation;
        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                newRot = hit.transform.rotation;
                if (transform.rotation.eulerAngles.y == 90)
                {
                    transform.rotation = Quaternion.Euler(-newRot.eulerAngles.z, transform.eulerAngles.y, transform.eulerAngles.z);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(newRot.eulerAngles.z, transform.eulerAngles.y, transform.eulerAngles.z);
                }
            }
        }

        // Rotate the player according to the direction
        if (MoveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, -90, transform.eulerAngles.z);
        }
        else if (MoveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(transform.eulerAngles.x, 90, transform.eulerAngles.z);
        }

        // Controls the animations
        if (_rigidbody.velocity.magnitude > 0)
        {
            if (IsSliding)
            {
                UpdateRunningAnimation?.Invoke(false);
            }
            else
            {
                UpdateRunningAnimation?.Invoke(true);
            }
        }
        else
        {
            UpdateRunningAnimation?.Invoke(false);

            UpdateSlideAnimation?.Invoke(false);
        }

        // Controls gamepad vibrations
        if (_rigidbody.velocity.magnitude > 0 && Gamepad.current != null)
        {
            if (IsSliding)
            {
                PlayerMain.GamepadShake(_rigidbody.velocity.magnitude / 100);
            }
            else
            {
                PlayerMain.StopGamepadShake();
            }
        }
        else if (Gamepad.current != null)
        {
            PlayerMain.StopGamepadShake();
        }
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        MoveDirection = new Vector3(ctx.ReadValue<Vector2>().x, 0, 0);
    }

    public void Jump(InputAction.CallbackContext ctx)
    {
        if (!ctx.started || !_canJump) return;
        transform.DOPunchScale(new Vector3(0, 1f, 0), 0.5f, 1, 0.5f);
        _rigidbody.AddForce(transform.up * _jumpForce, ForceMode.Impulse);
        _canJump = false;
        PhysicMat.frictionCombine = PhysicMaterialCombine.Minimum;
        UpdateJumpAnimation?.Invoke();
    }

    public void Slide(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            IsSliding = true;
            PhysicMat.dynamicFriction = 0.5f;
            PhysicMat.staticFriction = 0.5f;
            PhysicMat.frictionCombine = PhysicMaterialCombine.Minimum;
            UpdateSlideAnimation?.Invoke(true);
        }
        if (ctx.canceled)
        {
            UpdateSlideAnimation?.Invoke(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (!_canJump && (_squashSequence == null || !_squashSequence.IsPlaying())) {
                _squashSequence = DOTween.Sequence(transform.DOPunchScale(new Vector3(0, -0.5f, 0), 0.5f, 1, 0.5f)).Play();
            }
            _canJump = true;
        }
    }

    public void SetSliding()
    {
        IsSliding = true;
    }

    public void ResetSliding()
    {
        IsSliding = false;
    }

    public void ResetFrictions()
    {
        PhysicMat.dynamicFriction = 2f;
        PhysicMat.staticFriction = 2f;
        PhysicMat.frictionCombine = PhysicMaterialCombine.Average;
    }
}
