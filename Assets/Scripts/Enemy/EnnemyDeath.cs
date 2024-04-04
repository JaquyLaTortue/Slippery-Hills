using DG.Tweening;
using UnityEngine;

public class EnnemyDeath : MonoBehaviour
{
    public bool isDead = false;

    private bool alreadyDead = false;

    [SerializeField]
    private EnnemyMain _ennemyMain;

    private Rigidbody _rb;

    [SerializeField]
    private float _bumpForce = 10f;

    [SerializeField]
    private GameObject _deathEffect;

    private void Start()
    {
        _rb = _ennemyMain._ennemyMovement._rb;
    }

    // To test the death of the enemy
    private void Update()
    {
        if (isDead && !alreadyDead)
        {
            Die();
            alreadyDead = true;
        }
    }

    public void Die()
    {
        Debug.Log("Ennemy is dead");
        //_deathEffect.SetActive(true);
        _rb.AddForce(Vector3.up * _bumpForce, ForceMode.Impulse);
        _ennemyMain._ennemyMovement._collider.enabled = false;
        DOTween.Sequence(_ennemyMain.transform.DORotate(new Vector3(0, 0, 180), 3f))
            .Append(_ennemyMain.transform.DOPunchScale(Vector3.up, 3f, 2));
    }
}
