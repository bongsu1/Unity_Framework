using UnityEngine;

public class Manager
{
    public static GameManager Game { get { return GameManager.Instance; } }
    public static ResourceManager Resource { get { return ResourceManager.Instance; } }
    public static SceneManager Scene { get { return SceneManager.Instance; } }
    public static SoundManager Sound { get { return SoundManager.Instance; } }
    public static UIManager UI { get { return UIManager.Instance; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void Initialize()
    {
        GameManager.ReleaseInstance();
        ResourceManager.ReleaseInstance();
        SceneManager.ReleaseInstance();
        SoundManager.ReleaseInstance();
        UIManager.ReleaseInstance();

        GameManager.CreateInstance();
        ResourceManager.CreateInstance();
        SceneManager.CreateInstance();
        SoundManager.CreateInstance();
        UIManager.CreateInstance();
    }
}
