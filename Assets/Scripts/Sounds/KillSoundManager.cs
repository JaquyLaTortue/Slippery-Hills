using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KillSoundManager : MonoBehaviour
{
    [SerializeField]
    private PlayerMain _playerMain;

    [SerializeField]
    private DeathZone _deathZone;

    [SerializeField]
    private AudioSource _source;

    [SerializeField]
    private AudioSource _slideSource;

    [SerializeField]
    private AudioClip _killSound;

    [SerializeField]
    private AudioClip _explosionSound;

    [SerializeField]
    private AudioClip _slideSound;

    private int _count = 0;

    private Coroutine _resetCount;

    private void Start()
    {
        _playerMain.Collision.OnEnemyKilled += PlayKillSound;
        _deathZone.OnDeathZoneEnemy += PlayExplosionSound;
        _playerMain.Movement.IsSlidingEvent += PlaySlideSound;
    }

    public void PlayKillSound()
    {
        _count++;
        _source.pitch = 1 + _count * 0.1f;
        _resetCount = StartCoroutine(ResetCount());
        Debug.Log("Kill sound played");
        _source.PlayOneShot(_killSound);
    }

    public void PlayExplosionSound()
    {
        _source.PlayOneShot(_explosionSound);
    }

    private IEnumerator ResetCount()
    {
        yield return new WaitForSeconds(5f);
        _count = 0;
        _source.pitch = 1;
    }

    private void PlaySlideSound(bool slideState)
    {
        switch (slideState)
        {
            case true:
                if (!_slideSource.isPlaying)
                {
                    _slideSource.Play();
                }
                break;
            case false:
                _slideSource.Stop();
                break;
        }
    }
}
