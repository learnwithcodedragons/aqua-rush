using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public Timer GameTimer;
    public TMP_Text GameOverText;
    public GameObject ScoreBoard;

    void Start()
    {
        GameOverText.text = $"Wipe Out\nYou lasted { GameTimer.GetTimeElaapsed()} Seconds";
    }
}
