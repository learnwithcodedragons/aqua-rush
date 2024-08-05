using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{  
    public void OnPlay()
    {

        var adds = GameObject.Find("Ads").GetComponent<InterstitialAds>();

        if (PersistenceManager.Instance.ShouldShowAdvert())
        {
            adds.ShowAd();
        }

        PersistenceManager.Instance.IncrementNumberOfPlays();

        SceneManager.LoadScene("Game");
    }

    public void OnFirstPlay()
    {
        SceneManager.LoadScene("Game");
    }

    public void OnTutorial()
    {
        PersistenceManager.Instance.SetHasSeenTutorial(false);
        SceneManager.LoadScene("Game");
    }
}
