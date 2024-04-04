using System;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public event Action OnDeathZoneEnemy;
    public event Action OnDeathZonePlayer;

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            OnDeathZonePlayer?.Invoke();
            collision.gameObject.GetComponent<PlayerDeath>().Die();     // Change Die() to DeathZoneImpact()
        }

        if (collision.gameObject.CompareTag("Enemy")) {
            OnDeathZoneEnemy?.Invoke();
            collision.gameObject.GetComponent<EnemyDeath>().Die();     // Change Die() to DeathZoneImpact()
        }
    }
}
