using Cinemachine;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    [SerializeField]
    private PlayerMain _playerMain;

    [Header("Trail Parameters")]
    [SerializeField]
    private TrailRenderer _trailRenderer;
    [SerializeField]
    private float _trailMaxWidth = 0.75f;

    [Header("Speed VFX Parameters")]
    [SerializeField]
    private GameObject _speedVFX;
    private Material _speedVFXMaterial;

    [Header("Camera Zoom Parameters")]
    [SerializeField]
    private CinemachineVirtualCamera _camera;

    private void Start() {
        _trailRenderer.emitting = false;
        _speedVFXMaterial = _speedVFX.GetComponent<Renderer>().material;
    }

    private void FixedUpdate() {
        if (_playerMain.Movement.IsSliding) {
            _trailRenderer.emitting = true;
            if (_playerMain.Movement._rigidbody.velocity.magnitude / 100 > _trailMaxWidth) {
                _trailRenderer.startWidth = _trailMaxWidth;
            } else {
                _trailRenderer.startWidth = _playerMain.Movement._rigidbody.velocity.magnitude / 100;
            }
        } else {
            _trailRenderer.emitting = false;
        }

        _speedVFXMaterial.SetFloat("_Alpha", _playerMain.Movement._rigidbody.velocity.magnitude / 20);

        _camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 10 + _playerMain.Movement._rigidbody.velocity.magnitude / 2;
    }
}
