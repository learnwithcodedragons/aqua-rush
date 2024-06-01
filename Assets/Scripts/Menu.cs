using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void OnPlay()
    {
        Debug.Log("Play new game click");
        SceneManager.LoadScene("Game");
    }
}
