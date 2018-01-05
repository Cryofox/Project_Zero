using System;
using UnityEngine;
using System.Threading.Tasks;

using Terrain = ProjectZero.Model.World.Terrain;
namespace ProjectZero.Model.GameStateMachine
{
    class LevelEditorState : GameState
    {
        Terrain terrain;
        PlayerController controller;

        public override async Task Load()
        {
            percent = 0;
            Debug.Log("Setting up terrain");

            //Step 1 - Increment 
            terrain = new Terrain(20);
            terrain.Init();
            terrain.InitializeChunks(10);
            percent = 1;
            Debug.Log("Load Complete!");


            controller = new PlayerControllerMapEditor(terrain);
        }


        public override void Update(float timeDelta)
        {
            controller.Update(timeDelta);
            RenderGrid();
        }



        void RenderGrid()
        {
            terrain.RenderGridPoints();
            //For each Point X -> Draw the Line
            //terrain.RenderGridLines();
            terrain.RenderBoundingBoxes();
        }
    }
}
