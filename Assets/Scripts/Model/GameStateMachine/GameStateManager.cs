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

        private View activeView, requestedView;
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
        private GameView gameView;
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
    		loadingView = new LoadingView(GameObject.Find("Canvas/LoadingPanel"));
    		mainView = new MainMenuView(GameObject.Find("Canvas/MainMenuPanel"));
    		splashView = new SplashView(GameObject.Find("Canvas/SplashPanel"));
    		mapEditorView = new MapEditorView(GameObject.Find("Canvas/MapEditorPanel"));
            gameView = new GameView(GameObject.Find("Canvas/GamePanel"));
            //Hide all views
            loadingView.Hide();
            mainView.Hide();
            splashView.Hide();
            mapEditorView.Hide();
            gameView.Hide();
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
                    requestedView = mainView;
                    break;
                case States.LevelEditor:
                    requestedState = levelEditorState;
                    requestedView = mapEditorView;
                    break;
                case States.GamePlay:
                    requestedState = gamePlayState;
                    requestedView = gameView;
                    break;
            }

            //Step 1: Toggle Loading Screen
            loadingView.Show();
            //Start Async Unload -> GC Process
            UnloadOldAssets();
        }
    	

        public void Update(float timeDelta)
        {
            /*
        	if(activeState==null)
        	{
        		Debug.Log("ActiveState = NULL");
        		return;
        	}*/
            Debug.Log("EngineState:" + engineState);
            //GameLoop
            switch (engineState)
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
            loadingView.SetText(text);
            loadingView.SetPercent(percent);
        }
        private async void UnloadOldAssets()
        {
            engineState = EngineState.LoadingState;
            loadingState = LoadingState.UnloadingOldAssets;
            if (activeState!=null)
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
            //Toggle Views
            if(activeView!=null)
                activeView.Hide();
            requestedView.Show();

            activeView = requestedView;
            requestedView = null;
            //Cleanup States
            activeState = requestedState;
            requestedState = null;

            loadingView.Hide();
            //Clear Garbage/Unused
            engineState = EngineState.Active;
        }
    }
}
