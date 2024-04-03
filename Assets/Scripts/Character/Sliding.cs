using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <para>Author: Dave / GameDevelopment (YT)</para>
/// Lien: https://www.youtube.com/watch?v=SsckrYYxcuM
/// </summary>
public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform playerObj;
    private Rigidbody rb;
    private Move playerMove;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    public float slideTimer;

    public float slideYScale;
    private float startYScale;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerMove = GetComponent<PlayerMain>().MoveScript;

        startYScale = playerObj.localScale.y;
    }

    private void Update()
    {
        if (playerMove.IsCrouching && playerMove._verticalInput != 0)
            StartSlide();

        if (!playerMove.IsCrouching && playerMove.IsSliding)
            StopSlide();
    }

    private void FixedUpdate()
    {
        if (playerMove.IsSliding)
            SlidingMovement();
    }

    private void StartSlide()
    {
        playerMove.IsSliding = true;

        playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
        rb.drag = 0;

        slideTimer = maxSlideTime;
    }

    private void SlidingMovement()
    {
        // sliding normal
        if(!playerMove.CheckOnSlope() || rb.velocity.y > -0.1f)
        {
            rb.AddForce(playerMove.MoveDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.deltaTime;
        }

        // sliding down a slope
        else
        {
            rb.AddForce(playerMove.GetSlopeMoveDirection(playerMove.MoveDirection) * slideForce, ForceMode.Force);
        }

        if (slideTimer <= 0)
            StopSlide();
    }

    private void StopSlide()
    {
        playerMove.IsSliding = false;

        playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
        rb.drag = playerMove.groundDrag;
    }
}
