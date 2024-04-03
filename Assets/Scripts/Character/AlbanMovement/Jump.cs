using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    //Sets the jump force in the inspector
    public float jumpForce = 10f;
    //Defines if the player can jump or not
    [SerializeField] bool _canJump = true;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    //the player jumps by the JumpForce when the space bar is pressed 
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!ctx.started || !_canJump) return;
        rb.velocity = Vector3.up * jumpForce;
        _canJump = false;
    }

    //Allow the player to jump again when he touches the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _canJump = true;
        }
    }
}
