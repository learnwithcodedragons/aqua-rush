using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Timer GameTimer;
    public TMP_Text GameOverText;

    void Start()
    {
        GameOverText.text = $"Wipe Out\nYou lasted { GameTimer.GetTimer()} Seconds";
        StartCoroutine(WaitToLoadMenu());
    }

    private IEnumerator WaitToLoadMenu()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("Menu");
    } 
}
