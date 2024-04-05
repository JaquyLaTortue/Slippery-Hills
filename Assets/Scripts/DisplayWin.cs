using Cinemachine;
using UnityEngine;
using UnityEngine.VFX;

public class DisplayWin : MonoBehaviour
{
    [SerializeField]
    private PlayerMain _playerMain;

    [SerializeField]
    private Chronometer _chronometer;

    [SerializeField]
    private GameObject _finalVFX;

    [SerializeField]
    private GameObject _finalWallBound;

    [SerializeField]
    private GameObject _winText;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerMain.Movement.IsFinished = true;
            _chronometer.StopChronometer();

            _playerMain.VFX._camera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = 30;
            _finalVFX.SetActive(true);
            _finalVFX.GetComponent<VisualEffect>().Play();

            _winText.SetActive(true);
            _finalWallBound.SetActive(true);
        }
    }
}
