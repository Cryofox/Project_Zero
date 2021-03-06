﻿using System;
using System.Threading.Tasks;
using UnityEngine;
namespace ProjectZero.Model.GameStateMachine
{
    class GameState
    {
        protected float percent = 0;


        //Unloads the Scene and returns Completion Percentage
        virtual public async Task Unload() {
            percent = 0;
            Debug.Log("Unload Default...waiting");
            await TimeSpan.FromSeconds(2);
            percent = 1;
            Debug.Log("Unload Complete!");
        }
        //Loads the Scene and returns Completion Percentage
        virtual public async Task Load() {
            percent = 0;
            Debug.Log("Load Default...waiting");
            
            await TimeSpan.FromSeconds(2);
            percent = 1;
            Debug.Log("Load Complete!");
        }



        void TogglePlayerControl(bool enabled) { }
        public float Percent { get { return percent; } }
        public virtual void Update(float timeDelta) { }
    }

}