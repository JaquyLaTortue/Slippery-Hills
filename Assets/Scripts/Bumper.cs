using UnityEngine;

public class Bumper : MonoBehaviour
{
    [SerializeField]
    private float _bumperForce = 3000;

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Bumper hit : " + other.gameObject.GetComponent<Rigidbody>());
        other.gameObject.GetComponent<Rigidbody>().AddForce(other.gameObject.transform.up * _bumperForce, ForceMode.Impulse);
    }
}
