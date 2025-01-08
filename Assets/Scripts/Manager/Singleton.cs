using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static T _instance;
    public static T Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start() { Init(); }

    protected virtual void Init() { }

    public static void CreateInstance()
    {
        GameObject obj = new GameObject(typeof(T).Name, typeof(T));
        _instance = obj.GetOrAddComponent<T>();
    }

    public static void ReleaseInstance()
    {
        if (_instance == null)
            return;

        Destroy(_instance.gameObject);
        _instance = null;
    }
}
