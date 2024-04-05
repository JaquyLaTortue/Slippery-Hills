using System;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public event Action OnDeathZoneEnemy;

    public event Action OnDeathZonePlayer;

    [SerializeField]
    private GameObject _deathZonePlayerVFX;
    [SerializeField]
    private GameObject[] _deathZoneEnemiesVFX;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            OnDeathZonePlayer?.Invoke();
            _deathZonePlayerVFX.transform.position = collider.gameObject.transform.position;
            _deathZonePlayerVFX.GetComponent<ParticleSystem>().Play();
            collider.gameObject.GetComponent<PlayerDeath>().DeathZoneImpact();
        }

        if (collider.gameObject.CompareTag("Enemy"))
        {
            OnDeathZoneEnemy?.Invoke();
            //_deathZoneEnemyVFX.transform.position = collider.gameObject.transform.position;
            //_deathZoneEnemyVFX.GetComponent<ParticleSystem>().Play();
            collider.gameObject.GetComponent<EnemyDeath>().DeathZoneImpact();
        }
    }
}
