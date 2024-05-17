using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public TMP_Text _timerText;

    private float _timer = 0.0f;
    private bool _isTiming;



    private void Start()
    {
        _isTiming = true;
    }


    void FixedUpdate()
    {
        if (_isTiming)
        {
            _timer += Time.deltaTime;
            int seconds = (int)(_timer % 60);
            _timerText.text = seconds.ToString();
        }
    }

    public void ResetTimer()
    {
        _timer = 0;
    }

    public void StartTimer()
    {
        _isTiming = true;
    }

    public void StopTimer()
    {
        _isTiming = false;
    }

    public float GetTimer()
    {
        return _timer;
    }
}
