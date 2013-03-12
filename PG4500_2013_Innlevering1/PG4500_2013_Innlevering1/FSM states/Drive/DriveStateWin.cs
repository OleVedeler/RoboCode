using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG4500_2013_Innlevering1.FSM_states.Drive
{
    class DriveStateWin : State
    {
        public DriveStateWin() { }

        public override void Update()
        {
            //Do something cool
        }

        public override int GetNewState()
        {
            //Do not change
            return (int)DriveState.WIN;
        }
    }
}
