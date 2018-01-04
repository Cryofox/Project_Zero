using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectZero.Model.GameStateMachine;
public class MapEditorButton : MonoBehaviour {

    public void OnClick()
    {
        GameStateManager.Instance.LoadState(States.LevelEditor);
        Debug.Log("Changing State to LevelEditor...");
    } 	
}
