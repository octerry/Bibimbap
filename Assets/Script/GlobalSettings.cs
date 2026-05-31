using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalSettings : MonoBehaviour
{
    private static GlobalSettings instance = null;
    public static GlobalSettings Instance => instance;
    
    public static bool narratorEnabled = true;
    public static bool gameRunning = true;
    
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
		
        // Initialisation du Global Settings...
    }

    public static void LaunchPve()
    {
        SceneManager.LoadScene("Arena");
    }
}
