using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProjectZero.Model.GameStateMachine
{
    class GameState
    {
        float percent = 0;

        
        //Unloads the Scene and returns Completion Percentage
        public async Task Unload() { }
        //Loads the Scene and returns Completion Percentage
        public async Task Load() {
        }



        void TogglePlayerControl(bool enabled) { }
        public float Percent { get { return percent; } }
        public void Update(float timeDelta) { }
    }

}