using TMPro;
using UnityEngine;

public class SpeedUpdater : MonoBehaviour
{
    [SerializeField]
    private PlayerMain _playerMain;

    [SerializeField]
    private TMP_Text _speedText;

    private void Update()
    {
        _speedText.text = $"Speed: {Mathf.RoundToInt(_playerMain.Movement._rigidbody.velocity.magnitude)}";
    }
}
