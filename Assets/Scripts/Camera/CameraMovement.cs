using Cinemachine;
using DG.Tweening;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _camera;

    [SerializeField]
    private PlayerMain _playerMain;

    private float _shakeDuration = 0.5f;

    private void Start()
    {
        _playerMain.Collision.OnEnemyKilled += ShakeCam;
        _playerMain.Collision.OnPlayerDeath += ShakeCam;
    }

    public void ShakeCam()
    {
        CinemachineBasicMultiChannelPerlin perlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_AmplitudeGain = 3f;
        _shakeDuration = 0.5f;
    }

    private void Update() {
        if (_shakeDuration > 0) {
            _shakeDuration -= Time.deltaTime;
            if (_shakeDuration <= 0) {
                CinemachineBasicMultiChannelPerlin perlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                perlin.m_AmplitudeGain = 0;
            }
        }
    }
}
