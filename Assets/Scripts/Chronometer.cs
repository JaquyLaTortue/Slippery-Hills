using TMPro;
using UnityEngine;

public class Chronometer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _chronometerText;
    private float _time;
    private bool stop = false;

    public void ResetChronometer()
    {
        _time = 0;
        _chronometerText.text = "00:00";
    }

    public void StopChronometer()
    {
        stop = true;
    }

    private void Start()
    {
        _time = 0;
        _chronometerText.text = "00:00";
    }

    private void Update()
    {
        if (stop) { return; }
        _time += Time.deltaTime;
        _chronometerText.text = FormatTime(_time);
    }

    private string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}
