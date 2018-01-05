using System;
using UnityEngine;
using System.Threading.Tasks;

using Terrain = ProjectZero.Assets.Scripts.Model.World.Terrain;
namespace ProjectZero.Model.GameStateMachine
{
    class LevelEditorState : GameState
    {
        Terrain terrain;
        public override async Task Load()
        {
            percent = 0;
            Debug.Log("Setting up terrain");

            //Step 1 - Increment 
            terrain = new Terrain(2);
            terrain.Init();
            percent = 1;
            Debug.Log("Load Complete!");
        }


        public override void Update(float timeDelta)
        {
            RenderGrid();
        }



        void RenderGrid()
        {
            terrain.RenderGridPoints();
            //For each Point X -> Draw the Line
            terrain.RenderGridLines();

        }
    }
}
