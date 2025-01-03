using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    Dictionary<string, UnityEngine.Object> _resources = new();

    public T Load<T>(string path) where T : UnityEngine.Object
    {
        string key = $"{path}_{typeof(T)}";

        if (_resources.TryGetValue(key, out Object obj))
        {
            return obj as T;
        }
        else
        {
            T resource = Resources.Load<T>(path);
            if (resource == null)
            {
                Debug.Log($"load failure path: {path}");
                return null;
            }
            _resources.Add(key, resource);
            return resource;
        }
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = Load<GameObject>(path);
        if (prefab == null)
            return null;

        return Instantiate(prefab, parent);
    }
    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject obj = UnityEngine.Object.Instantiate(prefab, parent);
        obj.name = prefab.name; // 생성될 때 이름 뒤에 (Clone)을 제거하는 작업
        return obj;
    }
    public T Instantiate<T>(string path, Transform parent = null) where T : UnityEngine.Object
    {
        T prefab = Load<T>(path);
        if (prefab == null)
            return null;

        return Instantiate<T>(prefab, parent);
    }
    public new T Instantiate<T>(T prefab, Transform parent = null) where T : UnityEngine.Object
    {
        T obj = UnityEngine.Object.Instantiate<T>(prefab, parent);
        obj.name = prefab.name; // 생성될 때 이름 뒤에 (Clone)을 제거하는 작업
        return obj;
    }

    public void Destroy(GameObject obj)
    {
        if (obj == null)
            return;

        UnityEngine.Object.Destroy(obj);
    }
}
