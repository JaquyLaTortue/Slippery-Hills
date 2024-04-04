using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField]
    private float _bumperForce = 3000;

    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<Rigidbody>().AddForce(other.gameObject.transform.up * _bumperForce, ForceMode.Impulse);
    }
}
