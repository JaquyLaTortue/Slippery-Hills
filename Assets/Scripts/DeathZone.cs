using UnityEngine;

public class DeathZone : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Player")) {
            collision.gameObject.GetComponent<PlayerDeath>().Die();     // Change Die() to DeathZoneImpact()
        }
    }
}
