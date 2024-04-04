using DG.Tweening;
using System;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private PlayerMain _playerMain;

    [SerializeField]
    private float _deathBumpForce = 1000f;

    private Rigidbody _rb;

    private Collider _collider;

    public event Action OnDeathZoneImpact;

    private void Start()
    {
        _rb = _playerMain.Movement._rigidbody;
        _collider = GetComponent<Collider>();
        _playerMain.Collision.OnPlayerDeath += Die;
    }

    public void Die()
    {
        Debug.Log("Player is dead");
        _playerMain.Collision.enabled = false;
        _rb.velocity = Vector3.zero;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DORotate(new Vector3(0, 180, 0), 1f).OnComplete(() => DeathBump()))
            .Append(transform.DOPunchScale(Vector3.up, 3f, 2));
        sequence.Play();
    }

    private void DeathBump()
    {
        _rb.AddForce(Vector3.up * _deathBumpForce, ForceMode.Impulse);
        _collider.enabled = false;
    }

    public void DeathZoneImpact()
    {
        OnDeathZoneImpact?.Invoke();
        //Destroy(gameObject);
    }
}