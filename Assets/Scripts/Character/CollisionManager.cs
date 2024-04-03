using System;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;

    [SerializeField]
    private LayerMask _enemyLayer;

    public event Action OnEnnemyKilled;

    public event Action OnPlayerDeath;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == _enemyLayer)
        {
            switch (_playerMovement.IsSliding)
            {
                case true:
                    Debug.Log("Kill the ennemy");
                    OnEnnemyKilled?.Invoke();
                    collision.gameObject.GetComponent<EnnemyMain>()._ennemyDeath.Die();
                    break;
                case false:
                    Debug.Log("Kill the player");
                    OnPlayerDeath?.Invoke();
                    PlayerDie();
                    break;
            }
        }
    }

    private void PlayerDie()
    {
        // Fill in the code to kill the player
    }
}
