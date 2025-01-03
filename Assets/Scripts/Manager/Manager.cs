using UnityEngine;

public class Manager
{
    public static GameManager Game { get { return GameManager.Instance; } }
    public static ResourceManager Resource { get { return ResourceManager.Instance; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void Initialize()
    {
        GameManager.ReleaseInstance();
        ResourceManager.ReleaseInstance();

        GameManager.CreateInstance();
        ResourceManager.CreateInstance();
    }
}
