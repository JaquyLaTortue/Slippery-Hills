using System;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public event Action OnDeathZoneEnemy;

    public event Action OnDeathZonePlayer;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            OnDeathZonePlayer?.Invoke();
            collider.gameObject.GetComponent<PlayerDeath>().DeathZoneImpact();     // Change Die() to DeathZoneImpact()
        }

        if (collider.gameObject.CompareTag("Enemy"))
        {
            OnDeathZoneEnemy?.Invoke();
            collider.gameObject.GetComponent<EnemyDeath>().DeathZoneImpact();     // Change Die() to DeathZoneImpact()
        }
    }
}
