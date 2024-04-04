using DG.Tweening;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public bool isDead = false;

    private bool alreadyDead = false;

    [SerializeField]
    private EnemyMain _enemyMain;

    private Rigidbody _rb;

    [SerializeField]
    private float _bumpForce = 10f;

    private void Start()
    {
        _rb = _enemyMain._enemyMovement._rb;
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
        _rb.AddForce(Vector3.up * _bumpForce, ForceMode.Impulse);
        _enemyMain._enemyMovement._collider.enabled = false;
        DOTween.Sequence(_enemyMain.transform.DORotate(new Vector3(0, 0, 180), 3f))
            .Append(_enemyMain.transform.DOPunchScale(Vector3.up, 3f, 2));
    }
}
