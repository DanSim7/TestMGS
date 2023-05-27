using UnityEngine;

public static class SaveSystem
{
    private const string SoundOn = "SoundOn";
    private const string MusicOn = "MusicOn";
    private const string Volume = "Volume";

    public static bool IsSoundOn
    {
        get
        {
            if (PlayerPrefs.HasKey(SoundOn) == false)
                PlayerPrefs.SetInt(SoundOn, 0);

            var playerPrefsInt = PlayerPrefs.GetInt(SoundOn);
            
            if (playerPrefsInt == 0)
                return false;
            
            return true;
        }
        set
        {
            if (value == false)
                PlayerPrefs.SetInt(SoundOn, 0);
            else
                PlayerPrefs.SetInt(SoundOn, 1);
        }
    }
    
    public static bool IsMusicOn
    {
        get
        {
            if (PlayerPrefs.HasKey(MusicOn) == false)
                PlayerPrefs.SetInt(MusicOn, 0);

            var playerPrefsInt = PlayerPrefs.GetInt(MusicOn);
            
            if (playerPrefsInt == 0)
                return false;
            
            return true;
        }
        set
        {
            if (value == false)
                PlayerPrefs.SetInt(MusicOn, 0);
            else
                PlayerPrefs.SetInt(MusicOn, 1);
        }
    }

    public static float SoundMusicVolume
    {
        get
        {
            if (PlayerPrefs.HasKey(Volume) == false)
                PlayerPrefs.SetFloat(Volume, 0.3f);

            return PlayerPrefs.GetFloat(Volume);
        }
        set
        {
            var clampValue = Mathf.Clamp01(value);
            PlayerPrefs.SetFloat(Volume, clampValue);
        }
    }
}
