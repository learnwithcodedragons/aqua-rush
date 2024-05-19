
using UnityEngine;
using Dan.Main;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;

public class LeaderBoardManager : MonoBehaviour
{
    public TMP_Text[] _leadersText;
    public TMP_InputField _userNameInput;
    public Timer Timer;
    public GameObject LeaderBoard;
    public GameObject UserNameValidationText;
    public GameObject LeaderBoardEntry;

    private int _topTenScore;

    private void Start()
    {
        LoadEntries();
    }

    private void LoadEntries()
    {
        Leaderboards.AquaRush.GetEntries(entries =>
        {
            for (var i = 0; i < 10; i++)
            {
                var minutes = entries[i].Score / 60;
                var seconds = entries[i].Score % 60;
                var timeString = "0:00";
                if (entries[i].Score < 60)
                {
                    timeString = string.Format("{0:00}", seconds);
                }
                else
                {
                    timeString = string.Format("{0}:{1:00}", minutes, seconds);
                }

                _leadersText[entries[i].Rank - 1].text = $"{entries[i].Rank}.{entries[i].Username} {timeString}";
            }

            _topTenScore = entries.ToList().Find(e => e.Rank == 10).Score;
        });
    }
    public void UploadEntry()
    {
        var seconds = Timer.GetTimeElapsedInSeconds();

        var userName = _userNameInput.text;
        string pattern = @"^[a-zA-Z0-9]{1,8}$";
        Regex regex = new Regex(pattern);
        
        if (regex.IsMatch(userName)) 
        {
            UserNameValidationText.SetActive(false);
            LeaderBoardEntry.SetActive(false);
            Leaderboards.AquaRush.UploadNewEntry(_userNameInput.text, seconds, isSuccessful => {
                if (isSuccessful)
                {
                    LoadEntries();
                    LeaderBoard.SetActive(true);
                }
                else
                {
                    Debug.LogError("Error updating leaderboard");
                    LoadMenu();
                }
            });
        } 
        else 
        {
            UserNameValidationText.SetActive(true);
        } 
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public int GetTopTenScore()
    {
        return _topTenScore;
    }
}
