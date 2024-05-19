using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public Timer GameTimer;
    public TMP_Text GameOverText;

    void Start()
    {
        GameOverText.text = $"Wipe Out, You lasted { GameTimer.GetTimeElaapsed()} Seconds";

    }
}
