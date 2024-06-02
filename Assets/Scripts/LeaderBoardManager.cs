
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections.Generic;
using Unity.Services.Leaderboards;
using System;
using Unity.Services.Leaderboards.Models;
using Newtonsoft.Json;

public class LeaderBoardManager : MonoBehaviour
{
    public TMP_InputField _userNameInput;
    public Timer Timer;
    public GameObject LeaderBoard;
    public GameObject UserNameValidationText;
    public GameObject LeaderBoardEntry;

    const string LeaderboardId = "aqua-rush";

    private int _topTenScore;
    private int _playerBestScore;
    private Transform _entryContainer;
    private Transform _entryTemplate;
    private Transform _goldTemplate;
    private Transform _silverTemplate;
    private Transform _bronzeTemplate;
 
    private async void Start()
    {
        _entryContainer = LeaderBoard.transform.Find("Entry Container");
        _entryTemplate = _entryContainer.transform.Find("Template");
        _goldTemplate = _entryContainer.transform.Find("Template Gold");
        _silverTemplate = _entryContainer.transform.Find("Template Silver");
        _bronzeTemplate = _entryContainer.transform.Find("Template Bronze");
        _entryTemplate.gameObject.SetActive(false);
        _goldTemplate.gameObject.SetActive(false);
        _silverTemplate.gameObject.SetActive(false);
        _bronzeTemplate.gameObject.SetActive(false);

        await SetPlayerBestScore();
        Debug.Log("Player best score set...");
        await LoadEntries();
        Debug.Log("Successfully loaded entries");
    }

    private async Task LoadEntries()
    {
        _topTenScore = 0;
        var entries = await GetTopTenScores();
        if (entries is null) return;
        int i = 0;

        entries.ForEach( entry =>
        {
            Debug.Log("Adding entry: " + JsonConvert.SerializeObject(entry));
            var templateHeight = 30f;
            var rank = entry.Rank + 1;
            var entryTransform = rank switch
            {
                1 => Instantiate(_goldTemplate, _entryContainer),
                2 => Instantiate(_silverTemplate, _entryContainer),
                3 => Instantiate(_bronzeTemplate, _entryContainer),
                _ => Instantiate(_entryTemplate, _entryContainer),
            };
            Debug.Log("EntryTransform is null:" + entryTransform is null);
            RectTransform rectTransform = entryTransform.GetComponent<RectTransform>();
            RectTransform entryReactTranform = rectTransform;
            entryReactTranform.anchoredPosition = new Vector2(0, -templateHeight * i);

            var minutes = (int)(entry.Score / 60);
            var seconds = (int)entry.Score % 60;
            var timeString = "0:00";
            Debug.Log("Entry socre:" + entry.Score);
            if (entry.Score < 60)
            {
                timeString = string.Format("{0:00}", seconds);
            }
            else
            {
                timeString = string.Format("{0}:{1:00}", minutes, seconds);
            }

            if (i % 2 == 1)
            {
                var image = entryTransform.GetComponent<Image>();
                image.color = new Color32(255, 255, 225, 100);
            }

            var position = entryTransform.Find("Position");
            var positionText = position.GetComponent<TMP_Text>();
            positionText.text = AddPositionSuffix(entry.Rank + 1);

            var score = entryTransform.Find("Time");
            var scoreText = score.GetComponent<TMP_Text>();
            scoreText.text = timeString;


            var name = entryTransform.Find("Name");
            var nameText = name.GetComponent<TMP_Text>();

            var metadata = JsonUtility.FromJson<MetaData>(entry.Metadata);
            nameText.text = metadata.name;

            entryTransform.gameObject.SetActive(true);
            i++;

            if(entry.Score > _topTenScore)
            {
                _topTenScore = (int)entry.Score;
            }
        });     
    }

    public async void UploadEntry()
    {
        var seconds = Timer.GetTimeElapsedInSeconds();
        var userName = _userNameInput.text.Trim();
        string pattern = @"^[a-zA-Z0-9]{1,8}$";
        Regex regex = new Regex(pattern);
        
        if (regex.IsMatch(userName)) 
        {
            UserNameValidationText.SetActive(false);
            LeaderBoardEntry.SetActive(false);
            await AddScore(seconds, _userNameInput.text);
            await LoadEntries();
        } 
        else 
        {
            UserNameValidationText.SetActive(true);
        } 
    }

    private async Task SetPlayerBestScore()
    {
        var score = await GetPlayerScore();
        _playerBestScore = score;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public int GetTopTenScore()
    {
        return _topTenScore;
    }

    public int GetPlayersBestScore()
    {
        return _playerBestScore;
    }

    public async Task AddScore(int score, string name)
    {
        Debug.Log("Leaderboard uploading score...");
        var metadata = new Dictionary<string, string>();
        metadata.Add("name", name);
        var scoreResponse =
            await LeaderboardsService
            .Instance
            .AddPlayerScoreAsync(LeaderboardId, score, new AddPlayerScoreOptions { Metadata = metadata });

        Debug.Log("Leaderboard: " + JsonConvert.SerializeObject(scoreResponse));
    }

    public async Task<List<LeaderboardEntry>> GetTopTenScores()
    {
        try
        {
            var scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId, new GetScoresOptions { IncludeMetadata = true, Limit = 10 });
            Debug.Log("Leaderboard: " + JsonConvert.SerializeObject(scoresResponse.Results));
            return scoresResponse.Results;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
    }

    public async Task<int> GetPlayerScore()
    {
        try
        {
            var scoreResponse =
            await LeaderboardsService.Instance.GetPlayerScoreAsync(LeaderboardId, new GetPlayerScoreOptions { IncludeMetadata = true });
            return (int)scoreResponse.Score;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return 0;
        }
    }

    private string AddPositionSuffix(int number)
    {
        // Handle special cases for 11, 12, and 13
        if (number % 100 >= 11 && number % 100 <= 13)
        {
            return number + "th";
        }

        // Determine the suffix based on the last digit
        switch (number % 10)
        {
            case 1:
                return number + "st";
            case 2:
                return number + "nd";
            case 3:
                return number + "rd";
            default:
                return number + "th";
        }
    }
}

[Serializable]
public class MetaData
{
    public string name;
}
