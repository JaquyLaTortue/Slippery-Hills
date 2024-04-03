using UnityEngine;

public class KillSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _killSound;

    [SerializeField]
    private CollisionManager _collisionManager;

    private void Start()
    {
        if (_collisionManager == null)
        {
            GetComponent<CollisionManager>().OnEnnemyKilled += PlayKillSound;
        }
        else
        {
            Debug.LogError("No CollisionManager found");
        }
    }

    private void PlayKillSound()
    {
        _killSound.Play();
    }
}
