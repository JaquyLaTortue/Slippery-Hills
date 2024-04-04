using UnityEngine;

public class PlayerMain : MonoBehaviour
{
    [field: SerializeField]
    public PlayerMovement Movement { get; private set; }

    [field: SerializeField]
    public PlayerDeath Death { get; private set; }

    [field: SerializeField]
    public CollisionManager Collision { get; private set; }
}
