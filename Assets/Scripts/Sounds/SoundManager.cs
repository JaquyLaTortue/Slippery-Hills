using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SoundManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private PlayerMain _playerMain;

    [SerializeField]
    private DeathZone _deathZone;

    [Header("Audio Sources")]
    [SerializeField]
    private AudioSource _source;

    [SerializeField]
    private AudioSource _slideSource;

    [Header("Audio Clips")]
    [SerializeField]
    private AudioClip _killSound;

    [SerializeField]
    private AudioClip _explosionSound;

    [SerializeField]
    private AudioClip _slideSound;

    private int _count = 0;

    private Coroutine _resetCount;
    private Coroutine _fadeInSlide;
    private Coroutine _fadeOutSlide;

    private void Start()
    {
        _playerMain.Collision.OnEnemyKilled += PlayKillSound;
        _playerMain.VFX.FastSlideEvent += PlaySlideSound;
        _playerMain.Collision.OnPlayerDeath += PlayExplosionSound;
        _playerMain.Collision.OnPlayerDeath += StopSlideSound;

        _deathZone.OnDeathZoneEnemy += PlayExplosionSound;
        _deathZone.OnDeathZonePlayer += PlayExplosionSound;
    }

    public void PlayKillSound()
    {
        _count++;
        _source.pitch = 1 + _count * 0.1f;
        _resetCount = StartCoroutine(ResetCount());
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

    private void PlaySlideSound(bool slideState = false)
    {
        switch (slideState)
        {
            case true:
                if (!_playerMain.Death.isDead && !_slideSource.isPlaying)
                {
                    _fadeInSlide = StartCoroutine(FadeIn(_slideSource, 1f));
                }
                break;
            case false:
                if (_slideSource.isPlaying && _fadeInSlide == null && _fadeOutSlide == null)
                {
                    _fadeOutSlide = StartCoroutine(FadeOut(_slideSource, 1f));
                }
                break;
        }
    }

    private IEnumerator FadeIn(AudioSource source, float duration)
    {
        float startTimer = 0f;

        source.Play();

        while (startTimer < duration)
        {
            startTimer += Time.deltaTime;
            source.volume += startTimer / duration;
            yield return null;
        }

        _fadeInSlide = null;
    }

    private void StopSlideSound()
    {
        if (_fadeOutSlide == null)
        {
            _fadeOutSlide = StartCoroutine(FadeOut(_slideSource, 1f));
        }
    }

    private IEnumerator FadeOut(AudioSource source, float duration)
    {
        float startTimer = 0f;

        while (startTimer < duration)
        {
            startTimer += Time.deltaTime;
            source.volume -= 1 - startTimer / duration;
            yield return null;
        }

        source.Stop();
        _fadeOutSlide = null;
    }
}
