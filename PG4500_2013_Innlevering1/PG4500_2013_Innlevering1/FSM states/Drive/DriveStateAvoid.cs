using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PG4500_2013_Innlevering1
{
    class DriveStateAvoid : State
    {
        public DriveStateAvoid() { }

        public override void Update()
        {
            //Steeringbehavior do random stuff, try to avoid and wall avoidance and offset pursuit
            //Avoid bullets
            base.Update();
        }

        public override int GetNewState()
        {
            if(
            //If more energy; change to RAM
            //If much less energy; change to ESCAPE
            return base.GetNewState();
        }
    }
}
