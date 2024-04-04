using DG.Tweening;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField]
    private CollisionManager _collisionManager;

    [SerializeField]
    private PlayerMovement _playerMovement;

    private Rigidbody _rb;

    private Collider _collider;

    private float _deathBumpForce = 1000f;
    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _collisionManager.OnPlayerDeath += Die;
    }

    public void Die()
    {
        Debug.Log("Player is dead");
        _playerMovement.enabled = false;
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
}