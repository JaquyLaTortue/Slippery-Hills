using UnityEngine;

public class EnnemyMain : MonoBehaviour
{
    [field: SerializeField]
    public EnnemyMovement _ennemyMovement { get; private set; }

    [field: SerializeField]
    public EnnemyDeath _ennemyDeath { get; private set; }
}
