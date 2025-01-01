using UnityEngine;

public class Manager
{
    public static GameManager Game { get { return GameManager.Instance; } }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
    static void Initialize()
    {
        GameManager.ReleaseInstance();

        GameManager.CreateInstance();
    }
}
