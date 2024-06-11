using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    private bool _isAudioMute;
    private int _playCounter = 0;
    public bool IsAudioMute
    {
        get => _isAudioMute;
        set => _isAudioMute = value;
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

    public void IncrementNumberOfPlays()
    {
        _playCounter++;
    }

    public bool ShouldShowAdvert()
    {
        return _playCounter % 3 == 0 && _playCounter != 0;
    }

}
