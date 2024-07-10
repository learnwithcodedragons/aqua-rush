using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    private bool _isAudioMute;
    private int _playCounter = 0;
    private bool _hasSeenTutorial;

    private const string hasSeenTutorialKey = "hasSeenTutorial";

    public bool IsAudioMute
    {
        get => _isAudioMute;
        set => _isAudioMute = value;
    }

    public bool HasSeenTutorial
    {
        get => _hasSeenTutorial;
    }

    public static PersistenceManager Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        _hasSeenTutorial = PlayerPrefs.GetInt(hasSeenTutorialKey) == 1 ? true: false;
        Debug.Log($"Loading hasSeenTutorial with value {_hasSeenTutorial}");
    }

    public void SetHasSeenTutorial(bool hasSeenTutorial)
    {
        _hasSeenTutorial = hasSeenTutorial;
        PlayerPrefs.SetInt(hasSeenTutorialKey, hasSeenTutorial ? 1 : 0);
    }


    public void IncrementNumberOfPlays()
    {
        _playCounter++;
    }

    public bool ShouldShowAdvert()
    {
        return _playCounter % 3 == 0 && _playCounter != 0;
    }

  

}
