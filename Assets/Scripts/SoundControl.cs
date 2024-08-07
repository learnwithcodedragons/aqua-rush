using UnityEngine;
using UnityEngine.UI;

public class SoundControl : MonoBehaviour
{
    public Sprite SoundOffSprite;
    public Sprite SoundOnSprite;
    public GameObject Button;

    private AudioSource _audio;
    private Image _image;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
        _image = Button.GetComponent<Image>();
        _audio.mute = PersistenceManager.Instance.IsAudioMute;

        if (_audio.mute)
        {
            _image.sprite = SoundOffSprite;
        }
        else
        {
            _image.sprite = SoundOnSprite;
        }
    }

    public void ToggleSound()
    {
        _audio.mute = !_audio.mute;
        PersistenceManager.Instance.IsAudioMute = _audio.mute;

        if (_audio.mute)
        {
            _image.sprite = SoundOffSprite;
        } else
        {
            _image.sprite = SoundOnSprite;
        }
    }
}
