using UnityEngine;

public class EnnemyMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 100f;

    [SerializeField]
    private bool _goRight = true;

    [SerializeField]
    private Rigidbody _rb;

    private void Start()
    {
        if (_rb == null)
        { _rb = GetComponent<Rigidbody>(); }

        if (_rb == null)
        { Debug.LogError("Rigidbody is missing"); }
    }

    private void Update()
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
