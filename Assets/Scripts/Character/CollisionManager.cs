using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CollisionManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMain _playerMain;

    public event Action OnEnemyKilled;

    public event Action OnPlayerDeath;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            switch (_playerMain.Movement.IsSliding)
            {
                case true:
                    OnEnemyKilled?.Invoke();
                    if (Gamepad.current != null) { StartCoroutine(_playerMain.GamepadTimedShake(0.1f, 1f)); }
                    collision.gameObject.GetComponent<EnemyMain>()._ennemyDeath.Die();
                    break;
                case false:
                    OnPlayerDeath?.Invoke();
                    break;
            }
        }
    }
}
