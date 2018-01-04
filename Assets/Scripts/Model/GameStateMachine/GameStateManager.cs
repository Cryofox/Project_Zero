using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ProjectZero.Model.GameStateMachine
{
    enum States
    {
        MainMenu,
        LevelEditor,
        GamePlay
    }
    class GameStateManager
    {   
     	private GameState activeState;
    	private GameState requestedState;
    	enum EngineState
    	{
    		Active,
    		Paused,

    		LoadingState, //Similar to Switching States going from GameState -> GameState is the same as Loading a Level while in a Level
    		SavingState,  //Saving the State to a JSON File
    						//Configs: Preferences, Level Information, Progression, Editor Maps		
    	}
        enum LoadingState
        {
        	None,
        	UnloadingOldAssets,
        	LoadingNewAssets,
        	CollectingGarbage
        }

        EngineState engineState = EngineState.Active;
        LoadingState loadingState = LoadingState.None;


    	//Views
    	private LoadingView  loadingView; 
    	private SplashView   splashView; 
    	private MainMenuView mainView; 
    	private MapEditorView mapEditorView;     
    	//States
    	private MainMenuState mainMenuState;
    	private LevelEditorState levelEditorState;
    	private GamePlayState gamePlayState;	

    	private static GameStateManager instance;
    	public static GameStateManager Instance 
    	{ get 
    		{
    			if(instance == null)
    			{instance = new GameStateManager();}
    			return instance;
    		}
    	}
    	private GameStateManager()
    	{
    		SetupViews();
    		SetupStates();
    		LoadState(States.MainMenu);
    		
    	}

    	private void SetupViews()
    	{
    		loadingView = new LoadingView(GameObject.Find("LoadingPanel"));
    		mainView = new MainMenuView(GameObject.Find("MainMenuPanel"));
    		splashView = new SplashView(GameObject.Find("SplashPanel"));
    		mapEditorView = new MapEditorView(GameObject.Find("MapEditorPanel"));
    	}
    	private void SetupStates()
    	{
    		mainMenuState = new MainMenuState();
    		gamePlayState = new GamePlayState();
    		levelEditorState = new LevelEditorState();
    	}


    	public void LoadState(States state)
    	{
            //Switch to the corresponding State based upon enum request
            switch (state)
            {
                case States.MainMenu:
                    requestedState = mainMenuState;
                    break;
                case States.LevelEditor:
                    requestedState = levelEditorState;
                    break;
                case States.GamePlay:
                    requestedState = gamePlayState;
                    break;
            }

            //Step 1: Toggle Loading Screen
            loadingView.Show();
            //Start Async Unload -> GC Process
            UnloadOldAssets();

            //Configure States (for updating)
            //           engineState = EngineState.LoadingState;
            //            loadingState = LoadingState.UnloadingOldAssets;

            //Step 1A: Disable Control on activeGameState
            //activeState.TogglePlayerControl(false);


            // //Step 2: Set Text as Stage 1 / 3
            // //Start Unload Process 

            // //Step 3: Set Text as Stage 2 / 3
            // //Start Load Process of requested State

            // //Step 4: Set Text as Stage 3 / 3
            // //Garbage Collect

            // //Step 5: Make Level View Active

            // //Step 6: FadeOut LoadScreen
            // loadingView.FadeOut();
            // //Step 6A: Enable Control on activeGameState
            // activeState = requestedState;
            // requestedState.TogglePlayerControl(true);
        }
    	

        public void Update(float timeDelta)
        {
        	if(activeState==null)
        	{
        		Debug.Log("ActiveState = NULL");
        		return;
        	}

        	//GameLoop
        	switch(engineState)
        	{
        		case EngineState.Active:
        			activeState.Update(timeDelta);
                    break;
        		case EngineState.LoadingState:
        			switch(loadingState)
        			{
        				case LoadingState.UnloadingOldAssets:
                            UpdateLoadView("Step: 1/3 Unloading Assets...", activeState.Percent);
        				break;
        				case LoadingState.LoadingNewAssets:
                            UpdateLoadView("Step: 2/3 Loading Assets...", requestedState.Percent);
                            break;
        				case LoadingState.CollectingGarbage:
                            UpdateLoadView("Step: 3/3 Collecting Garbage...", 0);
                            break;
        			}
                    break;
        	}
        }



        // Load Routines
        private void UpdateLoadView(string text, float percent)
        {
            Debug.Log(text + " " + percent);
        }
        private async void UnloadOldAssets()
        {
            loadingState = LoadingState.UnloadingOldAssets;
            if(activeState!=null)
                await activeState.Unload();
            LoadNewAssets();
        }
        private async void LoadNewAssets()
        {
            loadingState = LoadingState.LoadingNewAssets;
            await requestedState.Load();
            CollectGarbage();
        }
        private async void CollectGarbage()
        {
            loadingState = LoadingState.CollectingGarbage;
            //Cleanup States
            activeState = requestedState;
            requestedState = null;
            //Clear Garbage/Unused

        }
    }
}
