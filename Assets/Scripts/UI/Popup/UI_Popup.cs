using UnityEngine;

public class UI_Popup : UI_Base
{
    protected override bool Init()
    {
        if (base.Init() == false)
            return false;

        Manager.UI.SetCanvas(gameObject);
        return true;
    }

    public void Close()
    {
        Manager.UI.ClosePopup(this);
    }
}
