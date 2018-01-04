using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEditorView : IView
{
    private GameObject panelGo;
    public MapEditorView(GameObject panel)
    {
        panelGo = panel;
    }

}
