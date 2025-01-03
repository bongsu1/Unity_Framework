using UnityEngine;

public class Manager
{
    public static GameManager Game { get { return GameManager.Instance; } }
    public static ResourceManager Resource { get { return ResourceManager.Instance; } }
    public static SceneManager Scene { get { return SceneManager.Instance; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void Initialize()
    {
        GameManager.ReleaseInstance();
        ResourceManager.ReleaseInstance();
        SceneManager.ReleaseInstance();

        GameManager.CreateInstance();
        ResourceManager.CreateInstance();
        SceneManager.CreateInstance();
    }
}
