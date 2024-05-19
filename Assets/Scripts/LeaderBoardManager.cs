
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

    private int _topTenScore;

    private void Start()
    {
        LoadEntries();
    }

    private void LoadEntries()
    {
        Leaderboards.AquaRush.GetEntries( entries =>
        {
            for (var i = 0; i < 10; i++)
            {
                _leadersText[entries[i].Rank - 1].text = $"{entries[i].Rank}.{entries[i].Username} {entries[i].Score}";
            }

            _topTenScore = entries.ToList().Find(e => e.Rank == 10).Score;
        });
    }
    public void UploadEntry()
    {
        var seconds = Timer.GetTimeElapsedInSeconds();

        var userName = _userNameInput.text;
        string pattern = @"^[a-zA-Z0-9]+$";
        Regex regex = new Regex(pattern);
        
        if (regex.IsMatch(userName)) 
        {
            UserNameValidationText.SetActive(false);
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
