using UnityEngine;

public class PersistenceManager : MonoBehaviour
{
    private bool _isAudioMute;
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
}
