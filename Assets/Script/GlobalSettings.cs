using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSettings : MonoBehaviour
{
    public enum SoundGroup {
        Global,
        SoundFX,
        Narrator,
        Music
    }
    
    private static GlobalSettings instance = null;
    public static GlobalSettings Instance => instance;
    
    public static bool narratorEnabled = true;
    public static bool gameRunning = true;

    public static float GlobalVolume = 1f;
    public static float SoundFxVolume = .5f;
    public static float NarratorVolume = .5f;
    public static float MusicVolume = .5f;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    public static void LaunchPve()
    {
        SceneManager.LoadScene("Arena");
    }

    public static void SetVolume(SoundGroup group, float value)
    {
        if (group == SoundGroup.Global)
        {
            GlobalVolume = value;
        }
        if (group == SoundGroup.Music)
        {
            MusicVolume = value;
        }
        if (group == SoundGroup.Narrator)
        {
            NarratorVolume = value;
        }
        if (group == SoundGroup.SoundFX)
        {
            SoundFxVolume = value;
        }
    }
}
