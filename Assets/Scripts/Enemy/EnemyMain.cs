using UnityEngine;

public class EnemyMain : MonoBehaviour
{
    [field: SerializeField]
    public EnemyMovement _enemyMovement { get; private set; }

    [field: SerializeField]
    public EnemyDeath _ennemyDeath { get; private set; }

    [field: SerializeField]
    public CollisionManager _collisionManager { get; private set; }
}
