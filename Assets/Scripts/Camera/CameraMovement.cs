using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    [SerializeField]
    private PlayerMain _playerMain;

    private void Start()
    {
        _playerMain.Collision.OnEnemyKilled += ShakeCam;
        _playerMain.Collision.OnPlayerDeath += ShakeCam;
    }

    public void ShakeCam()
    {
        _camera.transform.DOShakePosition(.5f, 1f, 10, 90, false, true);
    }
}
