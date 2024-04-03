using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Move : MonoBehaviour
{
    //Define at wich speed the player moves
    [Header("Speed")]
    public float InitialSpeed;
    public float Speed;
    private float desiredMoveSpeed;

    public float groundDrag;

    public float airMultiplier;
    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public bool IsSliding = false;
    public bool IsCrouching = false;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatisGround;
    bool grounded;

    [Header("Slope Check")]
    private RaycastHit slopeHit;
    public float maxSlopeAngle;

    [Header("Axis")]
    public float _verticalInput;
    public Vector3 MoveDirection;

    [Header("References")]
    private Rigidbody rb;
    public GameObject Player;

    private void Start()
    {
        Speed = InitialSpeed;
        rb = GetComponent<Rigidbody>();
    }

    public void SetSneakSpeed(float desiredSpeed)
    {
        Speed = desiredSpeed;
    }


    //gets the value of the move input
    public void OnMove(InputAction.CallbackContext context)
    {
        _verticalInput = context.ReadValue<Vector2>().x;
    }

    //will update the player position by the move input
    private void Update()
    {
        //ground check
        grounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, playerHeight * 0.5f + 0.2f, whatisGround);

        //handle drag
        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        MoveDirection = transform.forward * _verticalInput;
        if (grounded)
        {
            rb.AddForce(MoveDirection.normalized * Speed * 1f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(MoveDirection.normalized * Speed * 1f * airMultiplier, ForceMode.Force);
        }

        // check if desiredMoveSpeed has changed drastically
        //if (Mathf.Abs(desiredMoveSpeed - Speed) > 4f && Speed != 0) {
        //    StopAllCoroutines();
        //    StartCoroutine(SmoothlyLerpMoveSpeed());
        //}
        //else {
        //    Speed = desiredMoveSpeed;
        //}
    }

    public bool CheckOnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }
        return false;
    }

    public Vector3 GetSlopeMoveDirection(Vector3 direction) {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }

    private IEnumerator SmoothlyLerpMoveSpeed() {
        // smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - Speed);
        float startValue = Speed;

        while (time < difference) {
            Speed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            if (CheckOnSlope()) {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier * slopeAngleIncrease;
            }
            else
                time += Time.deltaTime * speedIncreaseMultiplier;

            yield return null;
        }

        Speed = desiredMoveSpeed;
    }
}