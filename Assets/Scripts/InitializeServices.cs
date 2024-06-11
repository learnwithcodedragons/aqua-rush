using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using UnityEngine;

public class InitializeServices : MonoBehaviour
{
    public TMP_Text Username;
    public TMP_Text AdvertisingId;
    public GameObject UserInfo;


    const string LeaderboardId = "aqua-rush";
    private bool _isUserInfoShowing;

    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Successfully initialized Unity Services...");
        await SignInAnonymously();
        Debug.Log("Successfully signed in...");
        Username.text = GetUsername();

        Application.RequestAdvertisingIdentifierAsync(
        (string advertisingId, bool trackingEnabled, string error) =>
        { Debug.Log("advertisingId " + advertisingId + " " + trackingEnabled + " " + error); }
    );
    }

    async Task SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += () =>
        {
            Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
        };
        AuthenticationService.Instance.SignInFailed += s =>
        {
            Debug.Log(s);
        };

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public void OnUserInfo()
    {
        _isUserInfoShowing = !_isUserInfoShowing;
        UserInfo.SetActive(_isUserInfoShowing);
    }

    private string GetUsername()
    {
      
        if(AuthenticationService.Instance.PlayerId != null)
        {
            return AuthenticationService.Instance.PlayerId;
        }

        return "N/A";
    }
}
