
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
    public GameObject Loading;
    public GameObject ErrorMessage;

    private int _topTenScore;
    private bool _leaderBoardReloading;
    private TMP_Text _loadingText;

    private void Start()
    {
        _loadingText = Loading.GetComponent<TMP_Text>();
        LoadEntries();
    }

    private void LoadEntries()
    {
        Leaderboards.AquaRush.GetEntries((entries) =>
        {
            if (_leaderBoardReloading)
            {
                _leaderBoardReloading = false;
                Loading.SetActive(false);
                LeaderBoard.SetActive(true);
            }

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
        }, error =>
        {
            Loading.SetActive(false);
            LeaderBoard.SetActive(false);
            ErrorMessage.SetActive(true);
        }
            );
    }
    public void UploadEntry()
    {
        var seconds = Timer.GetTimeElapsedInSeconds();

        var userName = _userNameInput.text;
        string pattern = @"^[a-zA-Z0-9]{1,8}$";
        Regex regex = new Regex(pattern);
        
        if (regex.IsMatch(userName)) 
        {
            Loading.SetActive(true);
            _loadingText.text = "Updating Leader Board...";
            UserNameValidationText.SetActive(false);
            LeaderBoardEntry.SetActive(false);
            LeaderBoard.SetActive(false);
            _leaderBoardReloading = true;
            Leaderboards.AquaRush.UploadNewEntry(_userNameInput.text, seconds, isSuccessful => {
                if (isSuccessful)
                {
                    _loadingText.text = "Reloading Leader Board...";
                    LoadEntries();
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
