using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using ProjectZero.Model.GameStateMachine;
public class Main : MonoBehaviour {
	GameStateManager gameStateManager;

	void Awake(){
		//Initialize the GameStateManager
		gameStateManager = GameStateManager.Instance;
        //Set the Default State
        gameStateManager.LoadState(States.LevelEditor);
	}
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		gameStateManager.Update(Time.deltaTime);
	}
}
