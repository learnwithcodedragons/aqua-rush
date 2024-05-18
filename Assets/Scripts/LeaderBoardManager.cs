
using UnityEngine;
using Dan.Main;
using TMPro;
using UnityEngine.SceneManagement;

public class LeaderBoardManager : MonoBehaviour
{
    public TMP_Text[] _leadersText;
    public TMP_InputField _userNameInput;
    public Timer Timer;
    public GameObject LeaderBoard;

    private void Start()
    {
        LoadEntries();
    }

    private void LoadEntries()
    {
        Leaderboards.AquaRush.GetEntries( entries => {
            for(var i = 0; i < 10; i++ )
            {
                _leadersText[entries[i].Rank - 1].text = $"{entries[i].Rank}.{entries[i].Username} {entries[i].Score}";
            }
        });
    }

    public void UploadEntry()
    {
        var seconds = Timer.GetTimeElapsedInSeconds();
        Leaderboards.AquaRush.UploadNewEntry(_userNameInput.text, seconds, isSuccessful => {
            if (isSuccessful)
            {
                LoadEntries();
                LeaderBoard.SetActive(true);
            }
            else
            {
                Debug.Log("Error uploading score");
            }
        });
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
