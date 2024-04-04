using UnityEngine;

public class PlayerTrail : MonoBehaviour
{
    [SerializeField]
    private PlayerMain _playerMain;
    [SerializeField]
    private TrailRenderer _trailRenderer;

    private void Start() {
        _trailRenderer.emitting = false;
    }

    private void Update() {
        if (_playerMain.Movement.IsSliding) {
            _trailRenderer.emitting = true;
            if (_playerMain.Movement._rigidbody.velocity.magnitude / 100 > 0.75f) {
                _trailRenderer.startWidth = 0.75f;
            } else {
                _trailRenderer.startWidth = _playerMain.Movement._rigidbody.velocity.magnitude / 100;
            }
            Debug.Log(_trailRenderer.startWidth);
        } else {
            _trailRenderer.emitting = false;
        }
    }
}
