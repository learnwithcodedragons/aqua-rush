using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnTutorial()
    {
        PersistenceManager.Instance.SetHasSeenTutorial(false);
        SceneManager.LoadScene("Game");
    }
}
