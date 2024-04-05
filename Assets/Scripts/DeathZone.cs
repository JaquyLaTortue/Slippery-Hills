using System;
using System.Collections;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public event Action OnDeathZoneEnemy;

    public event Action OnDeathZonePlayer;

    [SerializeField]
    private GameObject _deathZonePlayerVFX;
    [SerializeField]
    private EnemiesDeathVFXPool _enemiesDeathVFXPool;
    private GameObject _deathZoneEnemyVFX;

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
            _deathZoneEnemyVFX = _enemiesDeathVFXPool.PopVFX();
            _deathZoneEnemyVFX.transform.position = collider.gameObject.transform.position;
            _deathZoneEnemyVFX.GetComponent<ParticleSystem>().Play();
            collider.gameObject.GetComponent<EnemyDeath>().DeathZoneImpact();
            StartCoroutine(SendVFXBack());
        }
    }

    private IEnumerator SendVFXBack()
    {
        yield return new WaitForSeconds(3.0f);
        _enemiesDeathVFXPool.PushVFX(_deathZoneEnemyVFX);
    }
}
