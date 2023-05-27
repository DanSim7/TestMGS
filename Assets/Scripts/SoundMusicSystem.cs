using UnityEngine;

public class SoundMusicSystem : MonoBehaviour
{
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private AudioSource _musicSource;

    private bool _isSoundOn;
    
    [Space(10)]
    [SerializeField] private AudioClip _buttonSound;

    private void Start()
    {
        if (SaveSystem.IsMusicOn)
            PlayMusic();

        _soundSource.mute = !SaveSystem.IsSoundOn;
        _musicSource.mute = !SaveSystem.IsMusicOn;
    }

    public void PLayButtonSound()
    {
        _soundSource.PlayOneShot(_buttonSound);
    }

    public void UpdateVolume(float volume)
    {
        SaveSystem.SoundMusicVolume = volume;
        _soundSource.volume = volume;
        _musicSource.volume = volume;
    }

    public void ToggleMusic(bool isOn)
    {
        _musicSource.mute = !isOn;
        SaveSystem.IsMusicOn = isOn;
    }
    
    public void ToggleSound(bool isOn)
    {
        _soundSource.mute = !isOn;
        SaveSystem.IsSoundOn = isOn;
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void PlayMusic()
    {
        _musicSource.Play();
    }
}
