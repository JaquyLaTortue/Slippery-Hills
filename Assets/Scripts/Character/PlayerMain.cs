using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMain : MonoBehaviour
{
    [field: SerializeField]
    public PlayerMovement Movement { get; private set; }

    [field: SerializeField]
    public PlayerDeath Death { get; private set; }

    [field: SerializeField]
    public CollisionManager Collision { get; private set; }

    [field: SerializeField]
    public PlayerVFX VFX { get; private set; }

    [SerializeField]
    private DeathZone _deathZone;

    private void Start() {
        _deathZone.OnDeathZonePlayer += ShakeWhenExplosion;
        _deathZone.OnDeathZoneEnemy += ShakeWhenExplosion;
    }

    public IEnumerator GamepadTimedShake(float duration, float magnitude) {
        Gamepad.current.SetMotorSpeeds(magnitude, magnitude);
        yield return new WaitForSeconds(duration);
        Gamepad.current.SetMotorSpeeds(0, 0);
    }

    public void GamepadShake(float magnitude) {
        Gamepad.current.SetMotorSpeeds(magnitude / 2, magnitude);
    }

    public void StopGamepadShake() {
        Gamepad.current.SetMotorSpeeds(0, 0);
    }

    public void ShakeWhenExplosion() {
        StartCoroutine(GamepadTimedShake(0.5f, 1f));
    }
}
