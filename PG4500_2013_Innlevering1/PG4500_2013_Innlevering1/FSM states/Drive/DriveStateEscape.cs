using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;

namespace PG4500_2013_Innlevering1
{
    class DriveStateEscape : State
    {
        public DriveStateEscape(ref RoboData rData) 
        {
            this.rData = rData;
            steeringBehavior = new SteeringBehavior();
        }

        public override void Update()
        {
            //Steeringbehavior flee and wall avoidence
            //rData.velocity = steeringBehavior.Flee(eData.Position, rData.position, rData.MAX_SPEED, rData.velocity);
            rData.rotationRight = eData.Bearing;
            rData.ahead = 100;
        }

        public override int GetNewState()
        {
            int ret = (int)DriveState.ESCAPE;

            //If more energy; change to RAM
            if (rData.energy > eData.Energy) ret = (int)DriveState.RAM;

            //If the same energy; change to AVOID
            else if (rData.energy == eData.Energy) ret = (int)DriveState.AVOID;

            return ret;
        }
    }
}
