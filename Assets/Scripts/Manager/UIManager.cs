using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : Singleton<UIManager>
{
    int _order = -20;
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();

    protected override void Init()
    {
        base.Init();

        EnsureEventSystem();
    }

    public void EnsureEventSystem()
    {
        if (EventSystem.current != null)
            return;

        Manager.Resource.Instantiate<EventSystem>("UI/EventSystem");
    }

    public void SetCanvas(GameObject obj, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(obj);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowPopup<T>(string path = null, Transform parent = null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(path))
        {
            path = $"UI/Popup/{typeof(T).Name}";
        }

        GameObject obj = Manager.Resource.Instantiate(path);
        T popup = Util.GetOrAddComponent<T>(obj);
        _popupStack.Push(popup);

        if (parent == null)
            popup.transform.SetParent(transform);
        else
            popup.transform.SetParent(parent);

        return popup;
    }

    public void ClosePopup()
    {
        if (_popupStack.Count == 0)
            return;

        UI_Popup popup = _popupStack.Pop();
        Manager.Resource.Destroy(popup.gameObject);
        _order--;
    }
    public void ClosePopup(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("가장 최근에 열린 팝업이 아닙니다");
            return;
        }

        ClosePopup();
    }

    public void ClearPopup()
    {
        while(_popupStack.Count > 0)
        {
            ClosePopup();
        }
    }
}
