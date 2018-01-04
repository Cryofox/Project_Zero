using UnityEngine;


public class View
{
    protected GameObject panelGo;
    public View(GameObject panel)
    {
        this.panelGo = panel;
    }

    public void Show()
    {
        panelGo.active = true;
    }
    public void Hide()
    {
        panelGo.active = false;
    }
}
