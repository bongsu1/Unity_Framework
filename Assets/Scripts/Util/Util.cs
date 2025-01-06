using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject obj) where T : Component
    {
        T component = obj.GetComponent<T>();
        if (component == null)
        {
            component = obj.AddComponent<T>();
        }

        return component;
    }

    public static T FindChild<T>(GameObject obj, string name = null, bool recursive = false) where T : Component
    {
        if (obj == null)
            return null;

        if (recursive)
        {
            foreach(T component in obj.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name)
                    return component;
            }
        }
        else
        {
            Transform transform = obj.transform.Find(name);
            if (transform != null)
                return transform.GetComponent<T>();
        }

        return null;
    }
    public static GameObject FindChild(GameObject obj, string name = null, bool recursive = false)
    {
        return FindChild<Transform>(obj, name, recursive).gameObject;
    }
}
