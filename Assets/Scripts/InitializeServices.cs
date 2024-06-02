using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public class InitializeServices : MonoBehaviour
{
    private async void Awake()
    {
        await UnityServices.InitializeAsync();
        Debug.Log("Successfully initialized Unity Services...");
        await SignInAnonymously();
        Debug.Log("Successfully signed in...");
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
}
