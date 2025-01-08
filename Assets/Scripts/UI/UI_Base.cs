using System;
using System.Collections.Generic;
using UnityEngine;

public class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new();

    protected bool _init = false;

    protected virtual bool Init()
    {
        if (_init)
            return false;

        // 초기화 작업

        return _init = true;
    }

    private void Start()
    {
        Init();
    }

    protected void Bind<T>(Type enumType) where T : Component
    {
        if (enumType.IsEnum == false)
            return;

        string[] names = Enum.GetNames(enumType);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind : {names[i]}");
        }
    }

    protected T Get<T>(int idx) where T : Component
    {
        if (_objects.TryGetValue(typeof(T), out UnityEngine.Object[] objects) == false)
            return null;

        return objects[idx] as T;
    }

    public static void BindEvent(GameObject obj, Action action, EvtType type = EvtType.Click)
    {
        UI_EventHandler evt = obj.GetOrAddComponent<UI_EventHandler>();

        switch (type)
        {
            case EvtType.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            default:
                // EvtType을 추가할 시 수정하여 사용
                break;
        }
    }

    public enum EvtType { Click, }
}
