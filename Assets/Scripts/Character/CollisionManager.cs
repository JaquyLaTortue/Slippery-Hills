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
        if (collision.gameObject.CompareTag("Ennemy"))
        {
            switch (_playerMovement.IsSliding)
            {
                case true:
                    OnEnnemyKilled?.Invoke();
                    collision.gameObject.GetComponent<EnnemyMain>()._ennemyDeath.Die();
                    break;
                case false:
                    OnPlayerDeath?.Invoke();
                    break;
            }
        }
    }
}
