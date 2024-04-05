using Cinemachine;
using System;
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

    [Header("Walking VFX Parameters")]
    [SerializeField]
    private GameObject _walkingVFX;
    [SerializeField]
    private float _walkingVFXMaxEmissionRate = 10f;

    public event Action<bool> FastSlideEvent;

    private void Start()
    {
        _trailRenderer.emitting = false;
        _speedVFXMaterial = _speedVFX.GetComponent<Renderer>().material;
    }

    private void FixedUpdate()
    {
        if (_playerMain.Movement._rigidbody.velocity.magnitude > 0)
        {
            // Walking VFX
            if (_playerMain.Movement._canJump)
            {
                _walkingVFX.SetActive(true);
                if (_playerMain.Movement._rigidbody.velocity.magnitude > _walkingVFXMaxEmissionRate)
                {
                    _walkingVFX.GetComponent<ParticleSystem>().emissionRate = _walkingVFXMaxEmissionRate;
                }
                else
                {
                    _walkingVFX.GetComponent<ParticleSystem>().emissionRate = _playerMain.Movement._rigidbody.velocity.magnitude;

                }
            }

            // Trail
            if (_playerMain.Movement._rigidbody.velocity.magnitude > 10)
            {
                _trailRenderer.emitting = true;
                if (_playerMain.Movement._rigidbody.velocity.magnitude / 100 > _trailMaxWidth)
                {
                    _trailRenderer.startWidth = _trailMaxWidth;
                }
                else
                {
                    _trailRenderer.startWidth = _playerMain.Movement._rigidbody.velocity.magnitude / 100;
                }
                _speedVFXMaterial.SetFloat("_Alpha", _playerMain.Movement._rigidbody.velocity.magnitude / 20);
                FastSlideEvent?.Invoke(true);
            }
            else
            {
                _speedVFXMaterial.SetFloat("_Alpha", 0);
                FastSlideEvent?.Invoke(false);
                _walkingVFX.SetActive(false);
            }

            // Trail color
            float speedCap = 50;
            if (_playerMain.Movement._rigidbody.velocity.magnitude <= speedCap / 2) {

                byte colorValue = (byte)Mathf.RoundToInt(255 * (_playerMain.Movement._rigidbody.velocity.magnitude / (speedCap / 2)));
                Color color = new Color32(colorValue, 255, 0, 255);
                _trailRenderer.startColor = color;

            }
            else if (_playerMain.Movement._rigidbody.velocity.magnitude < speedCap) {

                byte colorValue = (byte)Mathf.RoundToInt(255 * (1 - (_playerMain.Movement._rigidbody.velocity.magnitude / speedCap)));
                Color color = new Color32(255, colorValue, 0, 255);
                _trailRenderer.startColor = color;

            }
            else {
                _trailRenderer.startColor = new Color(255, 0, 0);
            }
        }
        else
        {
            _trailRenderer.emitting = false;
        }


        _camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 10 + _playerMain.Movement._rigidbody.velocity.magnitude / 2;
    }

    public void DeathVFX()
    {
        _walkingVFX.SetActive(false);
    }
}
