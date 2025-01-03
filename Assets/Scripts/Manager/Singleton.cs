using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T instance;
    public static T Instance { get { return instance; } }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        Init();
    }

    protected virtual void Init() { }

    public static void CreateInstance()
    {
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));
        instance = Util.GetOrAddComponent<T>(obj);
    }

    public static void ReleaseInstance()
    {
        if (instance == null)
            return;

        Destroy(instance.gameObject);
        instance = null;
    }
}
