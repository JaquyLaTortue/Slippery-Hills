using TMPro;
using UnityEngine;

public class Chronometer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _chronometerText;
    private float _time;

    private void Start() {
        _time = 0;
        _chronometerText.text = "00:00";
    }

    private void Update() {
        _time += Time.deltaTime;
        _chronometerText.text = FormatTime(_time);
    }

    private string FormatTime(float time) {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }

    public void ResetChronometer() {
        _time = 0;
        _chronometerText.text = "00:00";
    }
}
