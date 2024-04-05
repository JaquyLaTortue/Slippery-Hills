using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private EnemyMain _ennemyMain;

    [SerializeField]
    private float _speed = 100f;

    [SerializeField]
    private bool _goRight = true;

    [field: SerializeField]
    public Rigidbody _rb { get; private set; }

    [field: SerializeField]
    public Collider _collider { get; private set; }

    private void Awake()
    {
        if (_rb == null)
        { _rb = GetComponent<Rigidbody>(); }
        if (_collider == null)
        { _collider = GetComponent<Collider>(); }

        if (_rb == null)
        { Debug.LogError("Rigidbody is missing"); }
    }

    private void Start()
    {
        if (_goRight && transform.rotation.y != 90 || !_goRight && transform.rotation.y != -90)
        {
            transform.rotation = Quaternion.Euler(0, _goRight ? 90 : -90, 0);
        }
    }

    private void Update()
    {
        if (!_ennemyMain._ennemyDeath.isDead)
        {
            if (_goRight)
            {
                _rb.velocity = new Vector3(1 * _speed * Time.deltaTime, _rb.velocity.y, 0);
            }
            else
            {
                _rb.velocity = new Vector3(-1 * _speed * Time.deltaTime, _rb.velocity.y, 0);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Enemy"))
        {
            _goRight = !_goRight;
            transform.rotation = Quaternion.Euler(0, _goRight ? 90 : -90, 0);
        }
    }
}
