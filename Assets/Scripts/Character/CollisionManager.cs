using System;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMain _playerMain;

    public event Action OnEnemyKilled;

    public event Action OnPlayerDeath;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ennemy"))
        {
            switch (_playerMain.Movement.IsSliding)
            {
                case true:
                    OnEnemyKilled?.Invoke();
                    collision.gameObject.GetComponent<EnemyMain>()._ennemyDeath.Die();
                    break;
                case false:
                    OnPlayerDeath?.Invoke();
                    break;
            }
        }
    }
}
