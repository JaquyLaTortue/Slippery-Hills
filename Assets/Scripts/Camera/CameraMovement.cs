using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private CollisionManager _collisionManager;

    private void Start()
    {
        _collisionManager.OnEnemyKilled += ShakeCam;
        _collisionManager.OnPlayerDeath += ShakeCam;
    }

    public void ShakeCam()
    {
        _camera.transform.DOShakePosition(.5f, 1f, 10, 90, false, true);
    }
}
