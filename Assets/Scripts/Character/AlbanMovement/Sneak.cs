using UnityEngine;
using UnityEngine.InputSystem;


public class Sneak : MonoBehaviour
{
    public bool Crouch { get; private set; }

    public float SneakHeight = 0.25f;

    public float SneakSpeed;

    [SerializeField] Move MoveScript;

    public void OnSneak(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            gameObject.transform.localScale = new Vector3(1f, SneakHeight, 1f);
            MoveScript.SetSneakSpeed(SneakSpeed);
            MoveScript.IsCrouching = true;
        }
        else if (ctx.canceled)
        {
            gameObject.transform.localScale = new Vector3(1f,1f, 1f);
            MoveScript.SetSneakSpeed(MoveScript.InitialSpeed);
            MoveScript.IsCrouching = false;
        }
        
    }
}
