using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santom;

namespace PG4500_2013_Innlevering1
{
    class DriveStateRam : State
    {
        public DriveStateRam() { }

        public override void Update()
        {
            //Steeringbehavior pursuit +/ seek and Wall avoidence
        }

        public override int GetNewState()
        {
            //If lower life change to ESCAPE
            //If the same life change to AVOID
            //If lost target change to IDLE
            return base.GetNewState();
        }
    }
}
