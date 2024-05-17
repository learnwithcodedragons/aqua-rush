using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text _timerText;

    private bool _isTiming;


    private float timeElapsed;
    private int minutes;
    private int seconds;
    private string timeString;




    private void Start()
    {
        _isTiming = true;
    }


    void Update()
    {
        if (_isTiming)
        {
            timeElapsed += Time.deltaTime;

            // Calculate minutes and seconds
            minutes = Mathf.FloorToInt(timeElapsed / 60F);
            seconds = Mathf.FloorToInt(timeElapsed % 60F);

            // Construct the time string based on the time elapsed
          
            if (timeElapsed < 60)
            {
                timeString = string.Format("{0:00}", seconds);
            }
            else
            {
                timeString = string.Format("{0}:{1:00}", minutes, seconds);
            }

            _timerText.text = timeString;
        }
    }

    public void ResetTimer()
    {
        timeElapsed = 0;
    }

    public void StartTimer()
    {
        _isTiming = true;
    }

    public void StopTimer()
    {
        _isTiming = false;
    }

    public string GetTimer()
    {
        return timeString;
    }
}
